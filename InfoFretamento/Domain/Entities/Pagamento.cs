namespace InfoFretamento.Domain.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public decimal ValorPago { get; set; }
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
        public DateOnly DataPagamento { get; set; }
    }
}
