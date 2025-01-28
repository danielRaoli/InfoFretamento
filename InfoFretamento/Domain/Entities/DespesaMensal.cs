namespace InfoFretamento.Domain.Entities
{
    public class DespesaMensal
    {
        public int Id { get; set; }
        public DateOnly DataPagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public string CentroDeCusto { get; set; } = string.Empty;
    }
}
