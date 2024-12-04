using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AdicionarServicoRequest : IBaseAdicionarRequest<Servico>
    {
        public string NomeServico { get; set; } = string.Empty;
        public Servico ToEntity()
        {
            return new Servico { NomeServico = NomeServico };   
        }
    }
}
