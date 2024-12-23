using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record AdicionarDespesaRequest :  IBaseAdicionarRequest<Despesa>
    {
        public DateTime DataEmissao { get; set; }
        public DateTime DataCompra { get; set; }
        public string OrigemPagamento { get; set; } = string.Empty; // 
        public string NumeroDocumento { get; set; } = string.Empty;
        public int ResponsavelId { get; set; }
        public int ViagemId { get; set; }
        public DateTime Vencimento { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorParcial { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;


        public Despesa ToEntity()
        {
            return new Despesa
            {
                DataEmissao = DateOnly.FromDateTime(DataEmissao),
                ViagemId = ViagemId,
                DataCompra = DateOnly.FromDateTime(DataCompra),
                OrigemPagamento = OrigemPagamento,
                NumeroDocumento = NumeroDocumento,
                CentroCusto = CentroCusto,
                ResponsavelId = ResponsavelId,
                Vencimento = DateOnly.FromDateTime(Vencimento),
                Pago = Pago,
                ValorTotal = ValorTotal,
                ValorParcial = ValorParcial,
            };
        }
    }
}
