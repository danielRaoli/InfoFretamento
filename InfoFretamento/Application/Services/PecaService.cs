using InfoFretamento.Application.Request.PecasRequest;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class PecaService(IBaseRepository<Peca> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BaseService<Peca, RegistrarPecaRequest, EditarPecaRequest>(repository, memoryCache, cacheManager)
    {

    }
}
