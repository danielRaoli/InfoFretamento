using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request.MotoristaRequest
{
    public record AtualizarMotoristaRequest : BaseAtualizarPessoaRequest<Motorista>
    {

        public Habilitacao Habilitacao { get; set; }

        public override Motorista UpdateEntity(Motorista entity)
        {
            entity.Nome = Nome;
            entity.DataNascimento = DateOnly.FromDateTime(DataNascimento);
            entity.Telefone = Telefone;
            entity.Documento = Documento;
            entity.Endereco = Endereco;
            entity.Cpf = Cpf;
            entity.Habilitacao = Habilitacao;

            return entity;
        }
    }
}
