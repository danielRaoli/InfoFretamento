using InfoFretamento.Application.Request;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class ViagemService(IBaseRepository<Viagem> repository) : BaseService<Viagem, AdicionarViagemRequest, AtualizarViagemRequest>(repository)
    {

    }
}
