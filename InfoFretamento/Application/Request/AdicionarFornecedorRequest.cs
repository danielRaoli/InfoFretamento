using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AdicionarFornecedorRequest : BasePessoaRequest, IBaseAdicionarRequest<Fornecedor>
    {
        public string Tipo { get; set; } = string.Empty;

        public Fornecedor ToEntity() => new Fornecedor
        {
            Nome = this.Nome,
            DataNascimento = this.DataNascimento,
            Telefone = this.Telefone,
            Documento = this.Documento,
            Endereco = this.Endereco,
            Cpf = this.Cpf,
            Tipo = this.Tipo
        };
    }
}
