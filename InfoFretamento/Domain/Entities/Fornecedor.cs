namespace InfoFretamento.Domain.Entities
{
    public class Fornecedor : Pessoa
    {
        public string NomeFantasia { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public List<Despesa> Despesas { get; set; } = [];
        public List<Receita> Receitas { get; set; } = [];
    }
}
