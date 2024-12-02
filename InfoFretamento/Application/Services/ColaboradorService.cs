using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services;

public class ColaboradorService(IBaseRepository<Colaborador> repository) : BaseService<Colaborador, AdicionarColaboradorRequest, AtualizarColaboradorRequest>(repository)
{

}
