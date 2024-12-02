using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{

    public record AtualizarColaboradorRequest : BaseAtualizarPessoaRequest<Colaborador>
    {
        public override Colaborador UpdateEntity(Colaborador entity)
        {
            entity.Nome = this.Nome;
            entity.DataNascimento = this.DataNascimento;
            entity.Telefone = this.Telefone;
            entity.Documento = this.Documento;
            entity.Endereco = this.Endereco;
            entity.Cpf = this.Cpf;

            return entity;
        }
    }

}
