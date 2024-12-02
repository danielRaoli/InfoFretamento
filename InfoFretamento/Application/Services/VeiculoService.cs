using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class VeiculoService(IBaseRepository<Veiculo> repository) : BaseService<Veiculo, AdicionarVeiculoRequest, AtualizarVeiculoRequest>(repository)
    {
       
    }
}
