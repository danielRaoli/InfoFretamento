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

        public async Task<Response<List<Receita?>>> GetAllWithFilterAsync(DateTime? dateStart = null, DateTime? dateEnd =null, int? receitaId = null)
        {


            var filters = new List<Expression<Func<Receita, bool>>>();

            if (receitaId == null)
            {
                filters.Add(d => d.DataCompra >= DateOnly.FromDateTime(dateStart.Value)); // Converte DateOnly para DateTime
                filters.Add(d => d.DataCompra <= DateOnly.FromDateTime(dateEnd.Value)); // Converte DateOnly para DateTime
            }
            else
            {
                filters.Add(d => d.Id == receitaId.Value);    
            }
       


            var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Viagem", "Viagem.Cliente", "Viagem.Veiculo", "Pagamentos" });


            return new Response<List<Receita?>>(response.ToList());
        }
    }
}
