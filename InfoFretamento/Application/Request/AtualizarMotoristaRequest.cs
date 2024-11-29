using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AtualizarMotoristaRequest : BasePessoaRequest
    {
        public int Id { get; set; }
        public Habilitacao Habilitacao { get; set; }
    }
}
