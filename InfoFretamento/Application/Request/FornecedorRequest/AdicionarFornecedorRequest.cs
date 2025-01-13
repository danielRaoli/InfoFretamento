using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.FornecedorRequest
{
    public record AdicionarFornecedorRequest : BasePessoaRequest, IBaseAdicionarRequest<Fornecedor>
    {
        public string Tipo { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;

        public Fornecedor ToEntity() => new Fornecedor
        {
            Nome = Nome,
            DataNascimento = DateOnly.FromDateTime(DataNascimento),
            Telefone = Telefone,
            Documento = Documento,
            Endereco = Endereco,
            Cpf = Cpf,
            Tipo = Tipo,
            NomeFantasia = NomeFantasia
            
        };
    }
}
