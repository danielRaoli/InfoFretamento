using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record AtualizarDespesaRequest : BaseAtualizarRequest<Despesa>
    {
        public DateTime DataPagamento { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public string CentroCusto { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public override Despesa UpdateEntity(Despesa entity)
        {
            entity.DataPagamento = DateOnly.FromDateTime(DataPagamento);
            entity.Vencimento = DateOnly.FromDateTime(Vencimento);
            entity.ValorTotal = this.ValorTotal;
            entity.CentroCusto = this.CentroCusto;
            entity.Descricao = Descricao;

            return entity;
        }
    }
}
