namespace InfoFretamento.Domain.Entities
{
    public class Peca
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
