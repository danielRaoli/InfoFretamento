using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.AdiantamentoRequests
{
    public class AdicionarAdiantamentoRequest : IBaseAdicionarRequest<Adiantamento>
    {
        public int ViagemId { get; set; }
        public string TipoVerba { get; set; } = string.Empty;
        public decimal Verba { get; set; }
        public decimal ValorDeAcerto { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public Adiantamento ToEntity() => new Adiantamento { TipoVerba = TipoVerba, Descricao = Descricao, ValorDeAcerto = ValorDeAcerto, Verba = Verba, ViagemId = ViagemId };
    }
}
