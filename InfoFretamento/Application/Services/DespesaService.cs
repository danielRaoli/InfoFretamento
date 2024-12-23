using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using System.Xml;

namespace InfoFretamento.Application.Services
{
    public class DespesaService(IBaseRepository<Despesa> repository, IMemoryCache cache) : BaseService<Despesa, AdicionarDespesaRequest, AtualizarDespesaRequest>(repository, cache)
    {
        private readonly IBaseRepository<Despesa> _repository = repository;
        private readonly IMemoryCache _memoryCache = cache;

        public async Task<Response<List<Despesa>>> GetAllWithFilterAsync(DateOnly dateStart, DateOnly dateEnd)
        {
            // Cria a chave única do cache
            string cacheKey = $"{typeof(Despesa).Name}_{dateStart.ToString()}_{dateEnd.ToString()}";

            // Usa o cache para armazenar ou recuperar os resultados
            var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                // Configura a expiração do cache
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);

                // Aplica os filtros
                var filters = new List<Expression<Func<Despesa, bool>>>();

                filters.Add(d => d.DataEmissao >= dateStart);


                filters.Add(d => d.DataEmissao <= dateEnd);


                // Busca os dados no repositório
                var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Viagem", "Responsavel" });
                return response.ToList(); // Retorna a lista como resultado do cache
            });

            return new Response<List<Despesa>>(result);
        }
    }
}
