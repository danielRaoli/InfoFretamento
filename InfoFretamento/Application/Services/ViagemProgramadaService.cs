using InfoFretamento.Application.Request.ViagemProgramadaRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ViagemProgramadaService(IBaseRepository<ViagemProgramada> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BaseService<ViagemProgramada, AdicionarViagemProgramadaRequest, AtualizarViagemProgramadaRequest>(repository, memoryCache, cacheManager)
    {
        private readonly IBaseRepository<ViagemProgramada> _repository = repository;
        public async Task<Response<ViagemProgramada?>> GetByIdIncludeAsync(int id)
        {

            // Chama o repositório genérico
            var viagem = await _repository.GetWithFilterAsync(
                        id: id,
                        includes: new string[]
                        {
                            "Passagens",
                            "Passagens.Passageiro",
                            "Veiculo"
                        }
                    );

            return viagem is not null
                ? new Response<ViagemProgramada?>(viagem)
                : new Response<ViagemProgramada?>(null, 404, "Nenhuma viagem encontrada");
        }
    }
}
