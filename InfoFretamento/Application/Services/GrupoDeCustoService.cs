using InfoFretamento.Application.Request;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class GrupoDeCustoService(IBaseRepository<GrupoDeCusto> repository) : BaseService<GrupoDeCusto, AdicionarGrupoDeCustoRequest, AtualizarGrupoCustoRequest>(repository)
    {
    }
}
