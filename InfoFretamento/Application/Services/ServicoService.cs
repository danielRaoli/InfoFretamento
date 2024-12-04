using InfoFretamento.Application.Request;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class ServicoService(IBaseRepository<Servico> repository) : BaseService<Servico, AdicionarServicoRequest, AtualizarServicoRequest>(repository)
    {
    }
}
