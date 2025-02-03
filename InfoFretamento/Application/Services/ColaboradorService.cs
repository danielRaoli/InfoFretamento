using InfoFretamento.Application.Request.ColaboradorRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class ColaboradorService( IBaseRepository<Colaborador> baseRepository, IMemoryCache cache,CacheManager cacheManager) : BasePessoaService<Colaborador, AdicionarColaboradorRequest, AtualizarColaboradorRequest>(baseRepository, cache, cacheManager)
    {
        private readonly IBaseRepository<Colaborador> _baserepository = baseRepository;
        public async Task<Response<Colaborador?>> GetWithIncludes(int id)
        {
            var response = await _baserepository.GetWithFilterAsync(id, new string[] { "Ferias" });
           
            return response != null ? new Response<Colaborador?>(response) : new Response<Colaborador?>(null, 404, "colaborador não encontrado");
        }


    }
}
