using InfoFretamento.Application.Request.AdiantamentoRequests;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class AdiantamentoService(IBaseRepository<Adiantamento> repository) : BaseService<Adiantamento, AdicionarAdiantamentoRequest,AtualizarAdiantamentoRequest>(repository)
    {
    }
}
