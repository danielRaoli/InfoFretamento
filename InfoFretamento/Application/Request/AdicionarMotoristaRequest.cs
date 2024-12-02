using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AdicionarMotoristaRequest : BasePessoaRequest, IBaseAdicionarRequest<Motorista>
    {
        public Habilitacao Habilitacao { get; set; }


        public Motorista ToEntity() => new Motorista { Nome = this.Nome, DataNascimento = this.DataNascimento, Telefone = this.Telefone, Documento = this.Documento, Endereco = this.Endereco, Cpf = this.Cpf, Habilitacao = this.Habilitacao };
    }
}
