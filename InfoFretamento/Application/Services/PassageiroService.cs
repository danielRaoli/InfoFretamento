using InfoFretamento.Application.Request.PassageiroRequest;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class PassageiroService(IBaseRepository<Passageiro> repository, IPessoaRepository<Passageiro> pessoaRepository, IMemoryCache cache) : BasePessoaService<Passageiro, AdicionarPassageiroRequest, AtualizarPassageiroRequest>(repository, pessoaRepository, cache)
    {
    }
}
