using InfoFretamento.Domain.Entities;
using System.Runtime.CompilerServices;

namespace InfoFretamento.Application.Request
{
    public record AdicionarClienteRequest : BasePessoaRequest, IBaseAdicionarRequest<Cliente>
    {
        public string Tipo { get; set; } = string.Empty;

        public Cliente ToEntity() => new Cliente { Nome = this.Nome, DataNascimento = this.DataNascimento, Telefone = this.Telefone, Documento = this.Documento, Endereco = this.Endereco, Cpf = this.Cpf, Tipo = this.Tipo };
    }
}
