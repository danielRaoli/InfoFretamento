using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services;

public class FornecedorService(IBaseRepository<Fornecedor> repository) : BaseService<Fornecedor, AdicionarFornecedorRequest, AtualizarFornecedorRequest>(repository)
{

}
