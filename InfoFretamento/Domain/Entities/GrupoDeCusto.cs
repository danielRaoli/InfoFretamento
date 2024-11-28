namespace InfoFretamento.Domain.Entities
{
    public class GrupoDeCusto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Despesa> Despesas { get; set; }
    }
}
