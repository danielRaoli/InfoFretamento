using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public  record AdicionarReceitaRequest : IBaseAdicionarRequest<Receita>
    {
        public DateTime DataPagamento { get; set; }
        public DateTime DataCompra { get; set; }
        public string OrigemPagamento { get; set; } = string.Empty; // 
        public string NumeroDocumento { get; set; } = string.Empty;
        public int ResponsavelId { get; set; }
        public int ViagemId { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;


        public Receita ToEntity()
        {
            return new Receita
            {
                DataPagamento = DateOnly.FromDateTime(DataPagamento),
                ViagemId = ViagemId,
                DataCompra = DateOnly.FromDateTime(DataCompra),
                OrigemPagamento = OrigemPagamento,
                NumeroDocumento = NumeroDocumento,
                CentroCusto = CentroCusto,
                Vencimento = DateOnly.FromDateTime(Vencimento),
                ValorTotal = ValorTotal,
            };
        }
    }
}
