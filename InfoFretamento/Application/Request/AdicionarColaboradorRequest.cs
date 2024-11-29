using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AdicionarColaboradorRequest : BasePessoaRequest
    {
        public  Colaborador ToEntity() => new Colaborador
        {
            Nome = this.Nome,
            DataNascimento = this.DataNascimento,
            Telefone = this.Telefone,
            Documento = this.Documento,
            Endereco = this.Endereco,
            Cpf = this.Cpf
        };
    }
}
