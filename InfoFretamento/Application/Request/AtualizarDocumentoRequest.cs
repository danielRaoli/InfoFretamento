using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AtualizarDocumentoRequest : BaseAtualizarRequest<Documento>
    {
        public DateTime Vencimento { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;

        public override Documento UpdateEntity(Documento entity)
        {
            entity.Vencimento = Vencimento.ToUniversalTime().AddHours(-3).Date;
            entity.Referencia = Referencia;
            entity.TipoDocumento = TipoDocumento;   

            return entity;
        }
    }
}
