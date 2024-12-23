using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ReceitaService(IBaseRepository<Receita> repository, IMemoryCache cache) : BaseService<Receita, AdicionarReceitaRequest, AtualizarReceitaRequest>(repository, cache)
    {
        private readonly IBaseRepository<Receita> _repository = repository;
        private readonly IMemoryCache _memorycache = cache;
        public async Task<Response<List<Receita?>>> GetAllWithFilterAsync(DateOnly dateStart , DateOnly dateEnd )
        {
            string cacheKey = $"{typeof(Receita).Name}_{dateStart.ToString()}_{dateEnd.ToString()}";

            var result = await _memorycache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                var filters = new List<Expression<Func<Receita, bool>>>();

                filters.Add(d => d.DataEmissao >= dateStart); // Converte DateOnly para DateTime


                filters.Add(d => d.DataEmissao <= dateEnd); // Converte DateOnly para DateTime

                var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Viagem", "Responsavel" });

                return response.ToList();
            });

            return new Response<List<Receita?>>(result.ToList());
        }
    }
}
