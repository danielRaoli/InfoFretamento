using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public class AdicionarDocumentoRequest : IBaseAdicionarRequest<Documento>
    {
        public DateTime Vencimento { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;

        public Documento ToEntity()
        {
            return new Documento { Referencia = Referencia, TipoDocumento = TipoDocumento, Vencimento = Vencimento.ToUniversalTime().AddHours(-3).Date };
        }
    }
}
