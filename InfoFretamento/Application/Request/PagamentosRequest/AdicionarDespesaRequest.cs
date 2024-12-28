using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record AdicionarDespesaRequest :  IBaseAdicionarRequest<Despesa>
    {
        public DateTime DataPagamento { get; set; }
        public DateTime DataCompra { get; set; }
        public string OrigemPagamento { get; set; } = string.Empty; 
        public int ResponsavelId { get; set; }
        public int ViagemId { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorParcial { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;


        public Despesa ToEntity()
        {
            return new Despesa
            {
                DataPagamento = DateOnly.FromDateTime(DataPagamento),
                ViagemId = ViagemId,
                DataCompra = DateOnly.FromDateTime(DataCompra),
                OrigemPagamento = OrigemPagamento,
                CentroCusto = CentroCusto,
                ResponsavelId = ResponsavelId,
                Vencimento = DateOnly.FromDateTime(Vencimento),
                ValorTotal = ValorTotal,
                ValorParcial = ValorParcial,
                FormaPagamento = FormaPagamento,    
            };
        }
    }
}
