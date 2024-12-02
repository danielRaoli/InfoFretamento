using InfoFretamento.Application.Request;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class DespesaService(IBaseRepository<Despesa> repository) : BaseService<Despesa, AdicionarDespesaRequest, AtualizarDespesaRequest>(repository)
    {

    }
}
