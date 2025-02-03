using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ReceitaService(IBaseRepository<Receita> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BaseService<Receita, AdicionarReceitaRequest, AtualizarReceitaRequest>(repository, memoryCache, cacheManager)
    {
        private readonly IBaseRepository<Receita> _repository = repository;

        public async Task<Response<List<Receita?>>> GetAllWithFilterAsync(int mes , int ano , string status, int? receitaId = null)
        {


            var filters = new List<Expression<Func<Receita, bool>>>();
            if(receitaId == null)
            {
                switch (status)
                {
                    case "todas":
                        filters.Add(r => r.Vencimento.Month == mes && r.Vencimento.Year == ano || r.Pagamentos.Any(p => p.DataPagamento.Month == mes && p.DataPagamento.Year == ano) || r.DataCompra.Month == mes && r.DataCompra.Year == ano || r.Pagamentos.Sum(p=> p.ValorPago) < r.ValorTotal);
                        break;
                    case "paga":
                        filters.Add(r => (r.Vencimento.Month == mes && r.Vencimento.Year == ano || r.Pagamentos.Any(p => p.DataPagamento.Month == mes && p.DataPagamento.Year == ano) || r.DataCompra.Month == mes && r.DataCompra.Year == ano) && r.Pagamentos.Sum(p => p.ValorPago) == r.ValorTotal);
                        break;
                    case "pendente":
                        filters.Add(r => (r.Vencimento.Month == mes && r.Vencimento.Year == ano || r.Pagamentos.Any(p => p.DataPagamento.Month == mes && p.DataPagamento.Year == ano) || r.DataCompra.Month == mes && r.DataCompra.Year == ano) && r.Pagamentos.Sum(p => p.ValorPago) < r.ValorTotal);
                        break;
                }
            }
            else
            {
                filters.Add(r => r.Id == receitaId);
            }

            var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Viagem", "Viagem.Cliente", "Viagem.Veiculo", "Pagamentos" });


            return new Response<List<Receita?>>(response.ToList());
        }
    }
}
