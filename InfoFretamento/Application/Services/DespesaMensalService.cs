using InfoFretamento.Application.Request.GastosMensais;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class DespesaMensalService(IBaseRepository<DespesaMensal> repository, IMemoryCache cache, CacheManager cacheManager): BaseService<DespesaMensal, AdicionarDespesaMensal, AtualizarDespesaMensal>(repository, cache, cacheManager)
    {

    }
}
