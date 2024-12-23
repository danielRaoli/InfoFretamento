using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.ServicoRequest
{
    public record AtualizarServicoRequest : BaseAtualizarRequest<Servico>
    {
        public string NomeServico { get; set; } = string.Empty;
        public override Servico UpdateEntity(Servico entity)
        {
            entity.NomeServico = NomeServico;
            return entity;
        }
    }
}
