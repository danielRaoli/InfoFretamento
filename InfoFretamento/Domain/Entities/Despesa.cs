namespace InfoFretamento.Domain.Entities
{
    public class Despesa
    {
        public int Id { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataCompra { get; set; }
        public string DestinoPagamento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public GrupoDeCusto GrupoCusto { get; set; }
        public int GrupoCustoId { get; set; }
        public int ResponsavelId { get; set; }
        public Veiculo Veiculo{ get; set; }
        public int VeiculoId { get; set; }
        public DateTime Vencimento { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
