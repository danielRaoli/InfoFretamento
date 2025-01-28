namespace InfoFretamento.Domain.Entities
{
    public class AdicionarPeca
    {
        public int Id { get; set; }
        public Peca Peca { get; set; }
        public int PecaId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoTotal { get; set; }
        public string TipoPagamento { get; set; } = string.Empty;
        public int Parcelas { get; set; }
        public DateOnly DataDeEntrada { get; set; }
    }
}
