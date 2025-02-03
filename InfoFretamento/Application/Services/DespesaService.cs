using InfoFretamento.Application.Request.PagamentoDespesaRequest;
using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure.Repositories;
using InfoFretamento.Migrations;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using System.Net;

namespace InfoFretamento.Application.Services
{
    public class DespesaService(IBaseRepository<Despesa> repository, IMemoryCache cache, CacheManager cacheManager, DespesaRepository despesaRepository, PagamentoRepository pagamentoRepository, BoletoRepository boletoRepository) : BaseService<Despesa, AdicionarDespesaRequest, AtualizarDespesaRequest>(repository, cache, cacheManager)
    {
        private readonly IBaseRepository<Despesa> _repository = repository;
        private readonly DespesaRepository _despesaRepository = despesaRepository;
        private readonly PagamentoRepository _pagamentoRepository = pagamentoRepository;
        private readonly BoletoRepository _boletoRepository = boletoRepository;
        private readonly IMemoryCache _memoryCache = cache;


        public async Task<Response<Despesa>> GetByEntityId(int entityid, string entityName)
        {


            // Busca os dados no repositório
            var response = await _despesaRepository.GetByEntityId(entityid, entityName);


            return new Response<Despesa>(response?.First() ?? null);
        }

        public override async Task<Response<Despesa?>> AddAsync(AdicionarDespesaRequest createRequest)
        {
            using var transaction = await _repository.BeginTransactionAsync();
            try
            {
                var despesa = createRequest.ToEntity();

                await _repository.AddAsync(despesa);

                if (createRequest.FormaPagamento.Equals("Boleto", StringComparison.OrdinalIgnoreCase))
                {
                    if (createRequest.VencimentosBoleto.Count() != createRequest.Parcelas)
                    {

                        await transaction.RollbackAsync();
                        return new Response<Despesa?>(null, 500, "O numero de parcelas nao corresponde ao numero de vencimentos");
                    }

                    var boletos = new List<Boleto>();
                    foreach (var vencimento in createRequest.VencimentosBoleto)
                    {
                        var boleto = new Boleto { DataEmissao = DateOnly.FromDateTime(DateTime.Now), DespesaId = despesa.Id, Pago = false, Referencia = $"Boleto da despesa ${despesa.Id}", Valor = despesa.ValorTotal / createRequest.Parcelas };
                        boletos.Add(boleto);
                    }

                    var result = await _boletoRepository.AddRange(boletos);
                    if (!result)
                    {
                        await transaction.RollbackAsync();
                        return new Response<Despesa?>(null, 500, "Nao foi possivel criar a quantidade de boletos");
                    }
                }

                await transaction.CommitAsync();
                return new Response<Despesa?>(despesa);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<Despesa?>(null, 500, "nao foi possivel criar a despesa");
            }
        }

        public async Task<Response<List<Despesa>>> GetAllWithFilterAsync(int? mes = null, int? ano = null, int? despesaCode = null, bool pendente = true)
        {


            // Aplica os filtros
            var filters = new List<Expression<Func<Despesa, bool>>>();

            if(despesaCode == null)
            {
                if (mes != null)
                {
                    filters.Add(d =>
                                    d.DataCompra.Month == mes && d.DataCompra.Year == ano || // Filtro pela data de compra
                                    d.Pagamentos.Any(p => p.DataPagamento.Month == mes && p.DataPagamento.Year == ano) || // Filtro pela lista de pagamentos
                                    (d.FormaPagamento == "Boleto" && d.Boletos.Any(b => b.DataPagamento.Value.Month == mes && b.DataPagamento.Value.Year == ano)));
                }
                if (pendente)
                {
                    filters.Add(d => d.FormaPagamento == "Boleto" ? d.ParcelasPagas < d.Parcelas : d.Pagamentos.Sum(p => p.ValorPago) < d.ValorTotal);
                }
                else
                {
                    filters.Add(d => d.FormaPagamento == "Boleto" ? d.ParcelasPagas == d.Parcelas : d.Pagamentos.Sum(p => p.ValorPago) == d.ValorTotal);
                }
            }
            else
            {
                filters.Add(d => d.Id == despesaCode);
            }




            // Busca os dados no repositório
            var response = await _repository.GetAllWithFilterAsync(filters, ["Boletos", "Pagamentos"]);


            return new Response<List<Despesa>>(response.ToList());
        }


        public async Task<Response<List<Despesa>>> GetAllPendentes(string status)
        {
            var filters = new List<Expression<Func<Despesa, bool>>>();

            if (status == "pendente")
            {
                filters.Add(d => d.Pagamentos.Sum(p => p.ValorPago) < d.ValorTotal && d.FormaPagamento != "Boleto" || d.Boletos.Count(b => b.Pago) < d.Parcelas && d.FormaPagamento == "Boleto");
            }
            else if (status == "paga")
            {
                filters.Add(d => d.Pagamentos.Sum(p => p.ValorPago) == d.ValorTotal || d.Boletos.Count(b => b.Pago) == d.Parcelas);
            }

            var despesas = await _repository.GetAllWithFilterAsync(filters, ["Boletos", "Pagamentos"]);

            return new Response<List<Despesa>>(despesas.ToList());

        }

        public async Task<Response<PagamentoDespesa?>> AdicionarPagamento(AdicionarPagamentoDespesa request)
        {
            var despesa = await _repository.GetWithFilterAsync(request.DespesaId,"Pagamentos");
            if(despesa == null)
            {
                return new Response<PagamentoDespesa?>(null, 404, "Despesa Nao encontrada");
            }

            if(despesa.Pago || despesa.ValorParcial + request.ValorPago > despesa.ValorTotal)
            {
                return new Response<PagamentoDespesa?>(null, 400, "Esta despesa ja foi paga, ou os valores de pagamento estao maiores que o valor total da despesa");
            }

            var entity = request.ToEntity();
            var result = await _pagamentoRepository.AdicionarPagamento(entity);

            if (!result)
            {
                return new Response<PagamentoDespesa?>(null, 500, "Erro ao tentar registrar");
            }
            return new Response<PagamentoDespesa?>(entity);
        }

        public async Task<Response<PagamentoDespesa?>> RemoverPagamento(int id)
        {
            var entity = await _pagamentoRepository.GetById(id);
            if (entity == null)
            {
                return new Response<PagamentoDespesa?>(null, 404, "pagamento nao encontrado");
            }
            var result = await _pagamentoRepository.RemovePagamento(entity);

            if (!result)
            {
                return new Response<PagamentoDespesa?>(null, 500, "Erro ao tentar remover");
            }
            return new Response<PagamentoDespesa?>(entity);
        }

        public async Task<Response<Boleto?>> PagarBoleto(int id)
        {
            var boleto = await _boletoRepository.GetById(id);
            if (boleto == null)
            {
                return new Response<Boleto?>(null, 404, "Erro ao tentar remover");
            }
            boleto.Pago = true;
            boleto.DataPagamento = DateOnly.FromDateTime(DateTime.UtcNow.AddHours(-3));
            var result = await _boletoRepository.Pagar(boleto);
            if (!result)
            {
                return new Response<Boleto?>(null, 500, "Erro ao tentar atualizar");
            }
            return new Response<Boleto?>(boleto);
        }
    }
}

