namespace InfoFretamento.Domain.Entities
{
    public class PagamentoDespesa
    {
        public int Id { get; set; }
        public decimal ValorPago { get; set; }
        public int DespesaId { get; set; }
        public Despesa Despesa { get; set; }
        public DateOnly DataPagamento { get; set; }
    }
}
