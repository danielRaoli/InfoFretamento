using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AtualizarFornecedorRequest : BaseAtualizarPessoaRequest<Fornecedor>
    {
        public string Tipo { get; set; } = string.Empty;

        public override Fornecedor UpdateEntity(Fornecedor entity)
        {
            entity.Nome = this.Nome;
            entity.DataNascimento = this.DataNascimento;
            entity.Telefone = this.Telefone;
            entity.Documento = this.Documento;
            entity.Endereco = this.Endereco;
            entity.Cpf = this.Cpf;
            entity.Tipo = this.Tipo;

            return entity;
        }
    }
}
