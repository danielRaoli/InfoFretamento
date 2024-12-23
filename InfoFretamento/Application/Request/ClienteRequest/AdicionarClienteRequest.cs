using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.ClienteRequest
{
    public record AdicionarClienteRequest : BasePessoaRequest, IBaseAdicionarRequest<Cliente>
    {
        public string Tipo { get; set; } = string.Empty;

        public Cliente ToEntity() => new Cliente { Nome = Nome, DataNascimento = DateOnly.FromDateTime(DataNascimento), Telefone = Telefone, Documento = Documento, Endereco = Endereco, Cpf = Cpf, Tipo = Tipo };
    }
}
