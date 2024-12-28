using InfoFretamento.Application.Request.AbastecimentoRequests;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class AbastecimentoService(IBaseRepository<Abastecimento> repository) : BaseService<Abastecimento,AdicionarAbastecimentoRequest,AtualizarAbastecimentoRequest>(repository)
    {
    }
}
