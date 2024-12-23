using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.ColaboradorRequest
{
    public record AdicionarColaboradorRequest : BasePessoaRequest, IBaseAdicionarRequest<Colaborador>
    {
        public Colaborador ToEntity() => new Colaborador
        {
            Nome = Nome,
            DataNascimento = DateOnly.FromDateTime(DataNascimento),
            Telefone = Telefone,
            Documento = Documento,
            Endereco = Endereco,
            Cpf = Cpf
        };
    }
}
