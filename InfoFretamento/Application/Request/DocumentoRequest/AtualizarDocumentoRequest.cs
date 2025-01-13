using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.DocumentoRequest
{
    public record AtualizarDocumentoRequest : BaseAtualizarRequest<Documento>
    {
        public DateTime Vencimento { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public bool Pendente { get; set; } 

        public override Documento UpdateEntity(Documento entity)
        {
            entity.Vencimento = DateOnly.FromDateTime(Vencimento);
            entity.Referencia = Referencia;
            entity.TipoDocumento = TipoDocumento;
            entity.Pendente = Pendente;
            return entity;
        }
    }
}
