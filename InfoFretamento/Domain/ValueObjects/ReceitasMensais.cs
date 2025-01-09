namespace InfoFretamento.Domain.ValueObjects
{
    public class ReceitasMensais
    {
        public string Month { get; set; } = string.Empty;
        public decimal Receitas { get; set; }
        public decimal Despesas { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}
