using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.ClienteRequest
{
    public record AtualizarClienteRequest : BaseAtualizarPessoaRequest<Cliente>
    {
        public string Tipo { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public override Cliente UpdateEntity(Cliente entity)
        {

            entity.Nome = Nome;
            entity.DataNascimento = DateOnly.FromDateTime(DataNascimento);
            entity.Telefone = Telefone;
            entity.Documento = Documento;
            entity.Endereco = Endereco;
            entity.Cpf = Cpf;
            entity.Tipo = Tipo;
            entity.NomeFantasia = NomeFantasia; 
            return entity;
        }
    }
}

