using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.ColaboradorRequest
{

    public record AtualizarColaboradorRequest : BaseAtualizarPessoaRequest<Colaborador>
    {
        public override Colaborador UpdateEntity(Colaborador entity)
        {
            entity.Nome = Nome;
            entity.DataNascimento = DateOnly.FromDateTime(DataNascimento);
            entity.Telefone = Telefone;
            entity.Documento = Documento;
            entity.Endereco = Endereco;
            entity.Cpf = Cpf;

            return entity;
        }
    }

}
