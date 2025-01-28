using InfoFretamento.Application.Request.ManutencaoRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ManutencaoService(IBaseRepository<Manutencao> repository, IMemoryCache memoryCache, CacheManager cacheManager, IBaseRepository<Despesa> baseDespesaRepository, DespesaRepository despesaRepository, BoletoRepository boletoRepository, IBaseRepository<Veiculo> veiculoRepository, IBaseRepository<Servico> servicoRepository) : BaseService<Manutencao, AdicionarManutencaoRequest, AtualizarManutencaoRequest>(repository, memoryCache, cacheManager)
    {
        private readonly IBaseRepository<Manutencao> _repository = repository;
        private readonly IBaseRepository<Despesa> _baseDespesaRepository = baseDespesaRepository;
        private readonly DespesaRepository _despesaRepository = despesaRepository;
        private readonly IBaseRepository<Veiculo> _veiculoRepository = veiculoRepository;   
        private readonly IBaseRepository<Servico> _servicoRepository = servicoRepository;    
        private readonly BoletoRepository _boletoRepository = boletoRepository;
        private readonly CacheManager _cacheManager = cacheManager;
        private readonly IMemoryCache _memoryCache = memoryCache;

        public  async Task<Response<ManutencaoResponse>> GetById(int id)
        {
            var manutencao = await _repository.GetWithFilterAsync(id, "Servico", "Veiculo");
            if (manutencao == null) {
                return new Response<ManutencaoResponse>(null, 404, "Manutencao nao encontrada, recarregue a pagina");
            }

            var despesa = await _despesaRepository.GetByEntityId(id, "Manutencao");


            var manutencaoResponse = new ManutencaoResponse { Manutencao = manutencao, Despesa = despesa?.FirstOrDefault()  };

            return new Response<ManutencaoResponse>(manutencaoResponse);

        }
        public async Task<Response<List<Manutencao>>> GetAllWithFilters(DateOnly startDate, bool? realizada, string veiculo )
        {
            var cacheKey = $"{typeof(Manutencao).Name}_GetAll_${startDate}-${realizada}";

            var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                _cacheManager.AddKey(cacheKey);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                var filters = new List<Expression<Func<Manutencao, bool>>>();

                if(realizada != null)
                {
                    filters.Add(m => m.Realizada == realizada);
                }


                if (veiculo != null)
                {
                    filters.Add(m => m.Veiculo.Prefixo.ToLower() == veiculo.ToLower());
                }

                if(realizada == null)
                {
                    filters.Add(m => m.DataLancamento >= startDate);
                }

                var response = await _repository.GetAllWithFilterAsync(filters: filters, includes: new string[] { "Veiculo", "Servico" });


                return response is null ?[] : response;
            });
          


            return new Response<List<Manutencao>>(result.ToList());

        }

        public override async Task<Response<Manutencao?>> UpdateAsync(AtualizarManutencaoRequest updateRequest)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                // Buscar a manutenção pelo ID
                var manutencao = await _repository.GetWithFilterAsync(updateRequest.Id, "Veiculo");

                if (manutencao == null)
                {
                    return new Response<Manutencao?>(null, 404, "Manutenção não encontrada.");
                }

                // Verificar se a manutenção já foi realizada
                if (manutencao.Realizada)
                {
                    return new Response<Manutencao?>(null, 400, "Não é possível modificar uma manutenção já realizada.");
                }

                // Atualizar as propriedades da manutenção com os dados do request
                updateRequest.UpdateEntity(manutencao);

                // Atualizar a manutenção no banco de dados
                await _repository.UpdateAsync(manutencao);

                // Verificar se a manutenção está sendo marcada como realizada no update
                if (updateRequest.Realizada)
                {
                    // Criar a despesa associada
                    var despesa = new Despesa
                    {
                        DataCompra = manutencao.DataLancamento,
                        Vencimento = updateRequest.TipoPagamento == "Boleto"
                            ? DateOnly.FromDateTime(updateRequest.Vencimentos[0])
                            : DateOnly.FromDateTime(updateRequest.VencimentoPagamento.Value),
                        EntidadeOrigem = "Manutencao",
                        EntidadeId = manutencao.Id,
                        ValorTotal = updateRequest.Custo,
                        FormaPagamento = updateRequest.TipoPagamento ?? "",
                        Parcelas = updateRequest.Parcelas ?? 1,
                        CentroCusto = "Manutencao",
                        Descricao = $"Despesa gerada para a manutenção do veículo {manutencao.Veiculo.Placa} - {manutencao.Veiculo.Prefixo}",
                    };

                    await _baseDespesaRepository.AddAsync(despesa);

                    // Gerar boletos se necessário
                    if (updateRequest.TipoPagamento.Equals("Boleto", StringComparison.OrdinalIgnoreCase))
                    {
                        if (updateRequest.Vencimentos.Count != updateRequest.Parcelas)
                        {
                            throw new ArgumentException("O número de vencimentos fornecido não corresponde ao número de parcelas.");
                        }

                        var boletos = updateRequest.Vencimentos.Select((vencimento, index) => new Boleto
                        {
                            Referencia = $"Manutencao-{manutencao.Id}-Parcela-{index + 1}",
                            DataEmissao = manutencao.DataLancamento,
                            Valor = updateRequest.Custo / updateRequest.Parcelas.Value,
                            Juros = 0,
                            DespesaId = despesa.Id,
                            Vencimento = DateOnly.FromDateTime(vencimento),
                            Pago = false
                        }).ToList();

                        await _boletoRepository.AddRange(boletos);
                        despesa.Boletos = boletos;
                    }

                    // Confirmar a transação
                    await transaction.CommitAsync();

                    // Limpar cache
                    _cacheManager.ClearAll($"{typeof(Manutencao).Name}");
                    _cacheManager.ClearAll($"{typeof(Despesa).Name}");

                    return new Response<Manutencao?>(manutencao);
                }

                // Confirmar a transação para as alterações
                await transaction.CommitAsync();

                // Limpar cache
                _cacheManager.ClearAll($"{typeof(Manutencao).Name}");

                return new Response<Manutencao?>(manutencao);
            }
            catch (Exception ex)
            {
                // Reverter transação em caso de erro
                await transaction.RollbackAsync();

                return new Response<Manutencao?>(null, 500, "Erro ao atualizar manutenção.");
            }
        }

        public async Task<Response<Manutencao?>> AddManutencaoAsync(AdicionarManutencaoRequest createRequest)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var veiculo = await _veiculoRepository.GetByIdAsync(createRequest.VeiculoId);
                var servico = await _servicoRepository.GetByIdAsync(createRequest.ServicoId);
                if ( veiculo is null || servico is null)
                {
                    return new Response<Manutencao?>(null, 404, "O Servico ou veiculo escolhido nao existe");
                }


                // Criar a entidade Manutencao a partir do request
                var manutencao = createRequest.ToEntity();

                // Adicionar a manutenção ao banco de dados
                await _repository.AddAsync(manutencao);

                manutencao.Veiculo = veiculo;
                manutencao.Servico = servico;

                if (createRequest.Realizada)
                {
                    // Criar a despesa associada
                    var despesa = new Despesa
                    {
                        DataCompra = manutencao.DataLancamento,
                        Vencimento = manutencao.TipoPagamento == "Boleto" ? DateOnly.FromDateTime(createRequest.Vencimentos[0]) : DateOnly.FromDateTime(createRequest.VencimentoPagamento.Value),
                        EntidadeOrigem = "Manutencao",
                        EntidadeId = manutencao.Id,
                        ValorTotal = createRequest.Custo,
                        FormaPagamento = createRequest.TipoPagamento,
                        Parcelas = createRequest.Parcelas.Value,
                        CentroCusto = "Manutencao",
                        Descricao = $"Despesa gerada para a manutenção do veículo {veiculo.Placa} - {veiculo.Prefixo}",
                    };

                    // Adicionar despesa ao banco de dados
                    await _baseDespesaRepository.AddAsync(despesa);



                    // Gerar boletos se a forma de pagamento for do tipo "Boleto"
                    if (createRequest.TipoPagamento.Equals("Boleto", StringComparison.OrdinalIgnoreCase))
                    {
                        // Validar se a lista de vencimentos tem o número correto de datas
                        if (createRequest.Vencimentos.Count != createRequest.Parcelas)
                        {
                            throw new ArgumentException("O número de vencimentos fornecido não corresponde ao número de parcelas.");
                        }

                        var boletos = createRequest.Vencimentos.Select((vencimento, index) => new Boleto
                        {
                            Referencia = $"Manutencao-{manutencao.Id}-Parcela-{index + 1}",
                            DataEmissao = manutencao.DataLancamento,
                            Valor = createRequest.Custo / createRequest.Parcelas.Value,
                            Juros = 0, // Assumindo sem juros
                            DespesaId = despesa.Id,
                            Vencimento = DateOnly.FromDateTime(vencimento),
                            Pago = false
                        }).ToList();

                        // Adicionar boletos ao banco de dados
                        await _boletoRepository.AddRange(boletos);

                        despesa.Boletos = boletos;
                        await transaction.CommitAsync();
                        _cacheManager.ClearAll($"{typeof(Manutencao).Name}");

                        _cacheManager.ClearAll($"{typeof(Despesa).Name}");


                         

                        // Retornar sucesso com a manutenção criada
                        return new Response<Manutencao?>(manutencao);
                    }


                    await transaction.CommitAsync();
                    _cacheManager.ClearAll($"{typeof(Manutencao).Name}");
                }

                // Confirmar a transação

                // Retornar sucesso com a manutenção criada

                await transaction.CommitAsync();
                _cacheManager.ClearAll($"{typeof(Manutencao).Name}");
                return new  Response<Manutencao?>(manutencao);
            }
            catch (Exception ex)
            {
                // Reverter a transação em caso de erro
                await transaction.RollbackAsync();

                // Retornar erro
                return new Response<Manutencao?>(null,500,$"Erro ao adicionar manutenção ");
            }
        }

        public override async Task<Response<Manutencao?>> RemoveAsync(int id)
        {
            using var transaction = await _repository.BeginTransactionAsync();
            try
            {
                var manutencao = await _repository.GetByIdAsync(id);
                if (manutencao is null)
                {
                    return new Response<Manutencao?>(null, 404, "Manutencao nao encontrada");
                }
                var manutencaoRemovida = await _repository.DeleteAsync(manutencao);
                if (manutencao.Realizada)
                {
                    var result = await _despesaRepository.DeleteAsync(manutencao.Id, "Manutencao");
                    if (!result)
                    {
                        await transaction.RollbackAsync();

                        return new Response<Manutencao?>(null, 500, "Erro ao tentar remover manutencao");

                    }
                    _cacheManager.ClearAll($"{typeof(Despesa).Name}");
                }


                await transaction.CommitAsync();
                _cacheManager.ClearAll($"{typeof(Manutencao).Name}");

                return new Response<Manutencao?>(null);
            }
            catch (Exception ex) {
                await transaction.RollbackAsync();
                return new Response<Manutencao?>(null, 500, "Erro ao tentar remover manutencao");
            }

        }

    }
}
