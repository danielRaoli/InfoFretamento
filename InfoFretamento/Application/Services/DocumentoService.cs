using InfoFretamento.Application.Request.DocumentoRequest;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class DocumentoService(IBaseRepository<Documento> repository) : BaseService<Documento, AdicionarDocumentoRequest, AtualizarDocumentoRequest>(repository)
    {

    }
}
