using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record AtualizarReceitaRequest : BaseAtualizarRequest<Receita>
    {
        public DateTime DataCompra { get; set; }
        public string OrigemPagamento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public int ResponsavelId { get; set; }
        public int ViagemId { get; set; }
        public DateTime Vencimento { get; set; }

        public decimal ValorTotal { get; set; }
        public decimal ValorParcial { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;

        public override Receita UpdateEntity(Receita entity)
        {
            entity.DataCompra = DateOnly.FromDateTime(DataCompra);
            entity.OrigemPagamento = this.OrigemPagamento;
            entity.NumeroDocumento = this.NumeroDocumento;
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
