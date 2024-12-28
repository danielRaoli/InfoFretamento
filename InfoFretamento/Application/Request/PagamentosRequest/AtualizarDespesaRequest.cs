using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record AtualizarDespesaRequest : BaseAtualizarRequest<Despesa>
    {
        public DateTime DataPagamento { get; set; }
        public string OrigemPagamento { get; set; } = string.Empty;
        public int ResponsavelId { get; set; }
        public int ViagemId { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorParcial { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;

        public override Despesa UpdateEntity(Despesa entity)
        {
            entity.DataCompra = DateOnly.FromDateTime(DataPagamento);
            entity.OrigemPagamento = this.OrigemPagamento;
            entity.ResponsavelId = this.ResponsavelId;
            entity.ViagemId = this.ViagemId;
            entity.Vencimento = DateOnly.FromDateTime(Vencimento);
            entity.ValorTotal = this.ValorTotal;
            entity.ValorParcial = this.ValorParcial;
            entity.FormaPagamento = this.FormaPagamento;
            entity.CentroCusto = this.CentroCusto;

            return entity;
        }
    }
}
