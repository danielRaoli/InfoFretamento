using InfoFretamento.Application.Request.GastosMensais;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class SalarioService(IBaseRepository<Salario> repository, IMemoryCache cache, CacheManager cacheManager, IBaseRepository<Motorista> motoristaRepository, IBaseRepository<Colaborador> colaboradorRepository) : BaseService<Salario,AdicionarSalarioRequest, AtualizarSalarioRequest>(repository,cache,cacheManager)
    {
        private readonly IBaseRepository<Salario> _repository = repository;
        private readonly IBaseRepository<Motorista> _motoristaRepository = motoristaRepository;
        private readonly IBaseRepository<Colaborador> _colaboradorRepository = colaboradorRepository;
        public override async Task<Response<Salario?>> AddAsync(AdicionarSalarioRequest createRequest)
        {
            Pessoa responsavel = await _motoristaRepository.GetByIdAsync(createRequest.ResponsavelId);
            if (responsavel == null) {
                responsavel = await _colaboradorRepository.GetByIdAsync(createRequest.ResponsavelId);
                if (responsavel == null) { return new Response<Salario?>(null, 404, "Trabalhador nao encontrado"); }
            }

            var entity = createRequest.ToEntity();
           

            var result = await _repository.AddAsync(entity);
            if (!result)
            {
                return new Response<Salario?>(null, 500, "Nao foi possivel registrar o salario");
            }

            entity.Responsavel = responsavel;
            return new Response<Salario?>(entity);
        }
    }
}
