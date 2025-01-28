using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.GastosMensais
{
    public record AtualizarDespesaMensal : BaseAtualizarRequest<DespesaMensal>
    {
        public DateTime DataPagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public string CentroDeCusto { get; set; } = string.Empty; 

        public override DespesaMensal UpdateEntity(DespesaMensal entity)
        {
            entity.ValorTotal = ValorTotal;
            entity.DataPagamento = DateOnly.FromDateTime(DataPagamento);
            entity.CentroDeCusto = CentroDeCusto;   
            return entity;
        }
    }
}
