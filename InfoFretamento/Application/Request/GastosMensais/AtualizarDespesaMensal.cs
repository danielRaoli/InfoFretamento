using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.GastosMensais
{
    public record AtualizarDespesaMensal : BaseAtualizarRequest<DespesaMensal>
    {
        public int DiaPagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public string CentroDeCusto { get; set; } = string.Empty; 

        public override DespesaMensal UpdateEntity(DespesaMensal entity)
        {
            entity.ValorTotal = ValorTotal;
            entity.DiaPagamento = DiaPagamento;
            entity.CentroDeCusto = CentroDeCusto;   
            return entity;
        }
    }
}
