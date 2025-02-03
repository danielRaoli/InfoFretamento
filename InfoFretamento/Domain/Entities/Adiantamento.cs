namespace InfoFretamento.Domain.Entities
{
    public class Adiantamento
    {
        public int Id { get; set; }
        public string TipoVerba { get; set; } = string.Empty;
        public decimal Verba { get; set; }
        public decimal ValorDeAcerto { get; set; }
        public decimal Diferenca => Verba - ValorDeAcerto;
        public string Descricao { get; set; } = string.Empty;
        public int ViagemId { get; set; }
        public Viagem Viagem { get; set; }
        
    }
}