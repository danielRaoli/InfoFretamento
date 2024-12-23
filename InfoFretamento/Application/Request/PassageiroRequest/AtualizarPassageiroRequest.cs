using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request.PassageiroRequest
{
    public record AtualizarPassageiroRequest : BaseAtualizarRequest<Passageiro>
    {
        public string Cartao { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public DocumentoCliente Documento { get; set; }
        public Endereco Endereco { get; set; }
        public string Cpf { get; set; } = string.Empty;

        public override Passageiro UpdateEntity(Passageiro entity)
        {
            entity.Cartao = Cartao;
            entity.Matricula = Matricula;
            entity.Nome = Nome;
            entity.DataNascimento = DataNascimento;
            entity.Telefone = Telefone;
            entity.Documento = Documento;
            entity.Endereco = Endereco;
            entity.Cpf = Cpf;

            return entity;
        }
    }
}
