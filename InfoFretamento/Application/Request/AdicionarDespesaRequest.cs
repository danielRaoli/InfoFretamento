using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public class AdicionarDespesaRequest : IBaseAdicionarRequest<Despesa>
    {

        public DateTime DataCompra { get; set; }
        public string DestinoPagamento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public int GrupoCustoId { get; set; }
        public int ResponsavelId { get; set; }
        public int VeiculoId { get; set; }
        public DateTime Vencimento { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public Despesa ToEntity()
        {
            return new Despesa
            {
                DataLancamento = DateTime.UtcNow.AddHours(-3).Date,
                DataCompra = DataCompra.Date,
                DestinoPagamento = DestinoPagamento,
                GrupoCustoId = GrupoCustoId,
                ResponsavelId = ResponsavelId,
                VeiculoId = VeiculoId,
                Vencimento = Vencimento.Date,
                Pago = Pago,
                ValorTotal = ValorTotal,
            };
        }
    }
}
