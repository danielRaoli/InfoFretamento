using InfoFretamento.Application.Request;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class DocumentoService(IBaseRepository<Documento> repository) : BaseService<Documento, AdicionarDocumentoRequest, AtualizarDocumentoRequest>(repository)
    {

    }
}
