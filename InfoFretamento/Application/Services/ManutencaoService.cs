using InfoFretamento.Application.Request.ManutencaoRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ManutencaoService(IBaseRepository<Manutencao> repository, IMemoryCache cache) : BaseService<Manutencao, AdicionarManutencaoRequest, AtualizarManutencaoRequest>(repository, cache)
    {
        private readonly IBaseRepository<Manutencao> _repository = repository;
        private readonly IMemoryCache _cache = cache;

        public async Task<Response<List<Manutencao>>> GetAllWithFilters(DateOnly startDate, string? situacao = null )
        {
            string cacheKey = $"{typeof(Manutencao).Name}_{startDate.ToString()}_{situacao ?? ""}";


            var result = await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {

                var filters = new List<Expression<Func<Manutencao, bool>>>();

                filters.Add(m => m.DataLancamento >= startDate);
                var response = await _repository.GetAllWithFilterAsync(filters: filters, includes: new string[] { "Veiculo", "Servico" });

                if (response != null && situacao != null)
                {
                    response = response.Where(m => m.Situacao.Equals(situacao));
                }


                return new List<Manutencao>(response.ToList());

            });

            return new Response<List<Manutencao>>(result);

        }
    }
}
