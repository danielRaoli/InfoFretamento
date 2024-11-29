namespace InfoFretamento.Application.Request
{
    public record AtualizarFornecedorRequest : BasePessoaRequest
    {
        public int Id { get; set; }
        public string TipoPessoa { get; set; } = string.Empty;
    }
}
