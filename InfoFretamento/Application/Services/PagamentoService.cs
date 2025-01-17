using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class PagamentoService(IBaseRepository<Pagamento> repository, IMemoryCache cache, CacheManager cacheManager) : BaseService<Pagamento,PagamentoRequest, AtualizarPagamentoRequest>(repository, cache, cacheManager)
    {

    }
}
