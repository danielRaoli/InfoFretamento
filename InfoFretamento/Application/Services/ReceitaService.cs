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

        public async Task<Response<List<Receita?>>> GetAllWithFilterAsync(int? mes = null, int? ano = null,bool pendente = true, int? receitaId = null)
        {


            var filters = new List<Expression<Func<Receita, bool>>>();
            if(receitaId == null)
            {
                if (mes != null) filters.Add(receita => receita.Vencimento.Month == receita.Vencimento.Month && receita.Vencimento.Year == ano);
                if (pendente)
                {
                    filters.Add(receita => receita.Pagamentos.Sum(r => r.ValorPago) < receita.ValorTotal);
                }
                else
                {
                    filters.Add(receita => receita.Pagamentos.Sum(r => r.ValorPago) == receita.ValorTotal);
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
