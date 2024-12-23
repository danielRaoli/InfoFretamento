using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.FornecedorRequest
{
    public record AtualizarFornecedorRequest : BaseAtualizarPessoaRequest<Fornecedor>
    {
        public string Tipo { get; set; } = string.Empty;

        public override Fornecedor UpdateEntity(Fornecedor entity)
        {
            entity.Nome = Nome;
            entity.DataNascimento = DateOnly.FromDateTime(DataNascimento);
            entity.Telefone = Telefone;
            entity.Documento = Documento;
            entity.Endereco = Endereco;
            entity.Cpf = Cpf;
            entity.Tipo = Tipo;

            return entity;
        }
    }
}
