using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request.MotoristaRequest
{
    public record AdicionarMotoristaRequest : BasePessoaRequest, IBaseAdicionarRequest<Motorista>
    {
        public Habilitacao Habilitacao { get; set; }


        public Motorista ToEntity() => new Motorista { Nome = Nome, DataNascimento = DateOnly.FromDateTime(DataNascimento), Telefone = Telefone, Documento = Documento, Endereco = Endereco, Cpf = Cpf, Habilitacao = Habilitacao };
    }
}
