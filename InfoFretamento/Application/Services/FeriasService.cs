using InfoFretamento.Application.Request.FeriasRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class FeriasService(IBaseRepository<Ferias> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BaseService<Ferias, AdicionarFeriasRequest, AtualizarFeriasRequest>(repository, memoryCache, cacheManager)
    {
        private readonly IBaseRepository<Ferias> _repository = repository;


        public async Task<Response<List<Ferias>>> GetAllWithFilterAsync(int year)
        {



            // Aplica os filtros
            var filters = new List<Expression<Func<Ferias, bool>>>();

            filters.Add(d => d.InicioFerias.Year == year);


            // Busca os dados no repositório
            var response = await _repository.GetAllWithFilterAsync(filters);


            return new Response<List<Ferias>>(response.ToList());
        }
    }
}
