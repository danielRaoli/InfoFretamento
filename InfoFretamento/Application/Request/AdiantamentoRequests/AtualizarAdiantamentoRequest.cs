using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.AdiantamentoRequests
{
    public record AtualizarAdiantamentoRequest : BaseAtualizarRequest<Adiantamento>
    {

        public string TipoVerba { get; set; } = string.Empty;
        public decimal Verba { get; set; }
        public decimal ValorDeAcerto { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public override Adiantamento UpdateEntity(Adiantamento entity)
        {
            entity.TipoVerba = TipoVerba;    
            entity.Verba = Verba;
            entity.Descricao = Descricao;   
            entity.ValorDeAcerto = ValorDeAcerto;
            return entity;  
        }
    }
}
