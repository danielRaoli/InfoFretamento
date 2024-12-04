using InfoFretamento.Application.Request;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class ManutencaoService(IBaseRepository<Manutencao> repository) : BaseService<Manutencao, AdicionarManutencaoRequest, AtualizarManutencaoRequest>(repository)
    {
    }
}
