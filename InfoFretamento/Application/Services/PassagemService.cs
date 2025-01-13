using InfoFretamento.Application.Request.PassagemRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class PassagemService(IBaseRepository<Passagem> repository, IMemoryCache cache, CacheManager cacheManager) : BaseService<Passagem, AdicionarPassagemRequest,AtualizarPassagemRequest>(repository, cache, cacheManager)
    {
        private readonly IBaseRepository<Passagem> _repository = repository;

        public async Task<Response<List<Passagem>>> GetAll(int viagemId)
        {
            var filters = new List<Expression<Func<Passagem, bool>>>();

            filters.Add(p => p.ViagemId == viagemId);

            var result = await _repository.GetAllWithFilterAsync(filters, ["Viagem", "Viagem.Veiculo"]);
            
            return new Response<List<Passagem>>(result.ToList());
        }
    }
}
