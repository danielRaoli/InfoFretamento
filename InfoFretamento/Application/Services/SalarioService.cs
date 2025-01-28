using InfoFretamento.Application.Request.GastosMensais;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class SalarioService(IBaseRepository<Salario> repository, IMemoryCache cache, CacheManager cacheManager) : BaseService<Salario,AdicionarSalarioRequest, AtualizarSalarioRequest>(repository,cache,cacheManager)
    {
    }
}
