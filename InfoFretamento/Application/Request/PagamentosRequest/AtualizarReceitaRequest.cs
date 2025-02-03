using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record AtualizarReceitaRequest : BaseAtualizarRequest<Receita>
    {


        public int ViagemId { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;


        public override Receita UpdateEntity(Receita entity)
        {

            entity.ViagemId = this.ViagemId;
            entity.Vencimento = DateOnly.FromDateTime(Vencimento);
            entity.ValorTotal = this.ValorTotal;
            entity.FormaPagamento = this.FormaPagamento;
            entity.CentroCusto = this.CentroCusto;


            return entity;
        }
    }
}
