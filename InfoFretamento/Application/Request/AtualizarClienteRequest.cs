using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AtualizarClienteRequest : BasePessoaRequest
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
