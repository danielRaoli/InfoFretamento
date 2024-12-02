using InfoFretamento.Application.Request;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class DepesaService(IBaseRepository<Despesa> repository) : BaseService<Despesa, AdicionarDespesaRequest, AtualizarDespesaRequest>(repository)
    {

    }
}
