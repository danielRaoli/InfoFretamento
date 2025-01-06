using InfoFretamento.Application.Request.AbastecimentoRequests;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class AbastecimentoService(IBaseRepository<Abastecimento> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BaseService<Abastecimento,AdicionarAbastecimentoRequest,AtualizarAbastecimentoRequest>(repository, memoryCache, cacheManager)
    {
    }
}
