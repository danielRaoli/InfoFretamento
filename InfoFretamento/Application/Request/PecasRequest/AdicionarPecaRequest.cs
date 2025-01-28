using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PecasRequest
{
    public class AdicionarPecaRequest : IBaseAdicionarRequest<AdicionarPeca>
    {
        public int PecaId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoTotal { get; set; }
        public string TipoPagamento { get; set; } = string.Empty;
        public int Parcelas { get; set; }
        public List<DateTime> Vencimentos { get; set; } = [];
        public DateTime? Vencimento { get; set; }

        public AdicionarPeca ToEntity()
        {
           return new AdicionarPeca { PecaId = PecaId, Quantidade = Quantidade, TipoPagamento = TipoPagamento,Parcelas = Parcelas,PrecoTotal = PrecoTotal, DataDeEntrada = DateOnly.FromDateTime(DateTime.Now) };
        }
    }
}
