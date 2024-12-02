using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class MotoristaService(IBaseRepository<Motorista> repository) : BaseService<Motorista, AdicionarMotoristaRequest, AtualizarMotoristaRequest>(repository)
    {
      
    }
}
