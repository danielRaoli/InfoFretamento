using InfoFretamento.Application.Request.PassagemRequest;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class PassagemService(IBaseRepository<Passagem> repository, IMemoryCache cache) : BaseService<Passagem, AdicionarPassagemRequest,AtualizarPassagemRequest>(repository, cache)
    {
    }
}
