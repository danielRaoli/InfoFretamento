using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request
{
    public record AtualizarMotoristaRequest : BaseAtualizarPessoaRequest<Motorista>
    {
   
        public Habilitacao Habilitacao { get; set; }

        public override Motorista UpdateEntity(Motorista entity)
        {
            entity.Nome = this.Nome;
            entity.DataNascimento = this.DataNascimento;
            entity.Telefone = this.Telefone;
            entity.Documento = this.Documento;
            entity.Endereco = this.Endereco;
            entity.Cpf = this.Cpf;
            entity.Habilitacao = this.Habilitacao;  

            return entity;
        }
    }
}
