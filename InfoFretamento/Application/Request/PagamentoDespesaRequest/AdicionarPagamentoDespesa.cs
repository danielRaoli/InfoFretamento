using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentoDespesaRequest
{
    public class AdicionarPagamentoDespesa : IBaseAdicionarRequest<PagamentoDespesa>
    {
        public decimal ValorPago { get; set; }
        public int DespesaId { get; set; }
        public DateTime? DataPagamento { get; set; }


        public PagamentoDespesa ToEntity()
        {
            return new PagamentoDespesa
            {
                ValorPago = ValorPago,
                DespesaId = DespesaId,
                DataPagamento = DateOnly.FromDateTime(DataPagamento ?? DateTime.UtcNow.AddHours(-3))
            };
        }
    }
}
