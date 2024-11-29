using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AdicionarFornecedorRequest : BasePessoaRequest
    {
        public string TipoPessoa { get; set; } = string.Empty;

        public Fornecedor ToEntity() => new Fornecedor
        {
            Nome = this.Nome,
            DataNascimento = this.DataNascimento,
            Telefone = this.Telefone,
            Documento = this.Documento,
            Endereco = this.Endereco,
            Cpf = this.Cpf,
            TipoPessoa = this.TipoPessoa
        };
    }
}
