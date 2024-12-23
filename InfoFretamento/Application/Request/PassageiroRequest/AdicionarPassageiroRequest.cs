using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request.PassageiroRequest
{
    public record AdicionarPassageiroRequest : BasePessoaRequest, IBaseAdicionarRequest<Passageiro>
    {

        public string Cartao { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;

        public Passageiro ToEntity()
        {
            return new Passageiro
            {
                Cartao = Cartao,
                Matricula = Matricula,
                Nome = Nome,
                DataNascimento = DateOnly.FromDateTime(DataNascimento),
                Telefone = Telefone,
                Documento = Documento,
                Endereco = Endereco,
                Cpf = Cpf
            };
        }
    }
}
