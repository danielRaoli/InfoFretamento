using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AtualizarDespesaRequest : BaseAtualizarRequest<Despesa>
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

        public override Despesa UpdateEntity(Despesa entity)
        {
            entity.DataLancamento = DateTime.UtcNow.AddHours(-3).Date;
            entity.DataCompra = DataCompra.Date;
            entity.DestinoPagamento = DestinoPagamento;
            entity.GrupoCustoId = GrupoCustoId;
            entity.ResponsavelId = ResponsavelId;
            entity.VeiculoId = VeiculoId;
            entity.Vencimento = Vencimento.Date;
            entity.Pago = Pago;
            entity.ValorTotal = ValorTotal;

            return entity;
        }
    }
}
