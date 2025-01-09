using InfoFretamento.Application.Request.ColaboradorRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class ColaboradorService( IBaseRepository<Colaborador> baseRepository, IPessoaRepository<Colaborador> repository, IMemoryCache cache,CacheManager cacheManager) : BasePessoaService<Colaborador, AdicionarColaboradorRequest, AtualizarColaboradorRequest>(baseRepository,repository, cache, cacheManager)
    {
        private readonly IBaseRepository<Colaborador> _baserepository = baseRepository;
        private readonly IPessoaRepository<Colaborador> _pessoaRepository = repository;
        public async Task<Response<Colaborador?>> GetWithIncludes(int id)
        {
            var response = await _baserepository.GetWithFilterAsync(id, new string[] { "Ferias" });
           
            return response != null ? new Response<Colaborador?>(response) : new Response<Colaborador?>(null, 404, "colaborador não encontrado");
        }

        public async Task<Response<List<Colaborador>>> GetAllByName(string? name =null)
        {
            var response = await _pessoaRepository.GetAllNameContains(name);
            return  new Response<List<Colaborador>>(response.ToList());
        }
    }
}
