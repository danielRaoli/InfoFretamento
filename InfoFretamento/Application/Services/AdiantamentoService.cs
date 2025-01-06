using InfoFretamento.Application.Request.AdiantamentoRequests;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class AdiantamentoService(IBaseRepository<Adiantamento> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BaseService<Adiantamento, AdicionarAdiantamentoRequest,AtualizarAdiantamentoRequest>(repository,memoryCache, cacheManager)
    {
    }
}
