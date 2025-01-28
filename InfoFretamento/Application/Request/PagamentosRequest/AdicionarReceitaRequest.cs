using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public  record AdicionarReceitaRequest : IBaseAdicionarRequest<Receita>
    {
        public string OrigemPagamento { get; set; } = string.Empty; 
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
               
                ViagemId = ViagemId,
                DataCompra = DateOnly.FromDateTime(DateTime.Now),
                OrigemPagamento = OrigemPagamento,
                NumeroDocumento = NumeroDocumento,
                CentroCusto = CentroCusto,
                Vencimento = DateOnly.FromDateTime(Vencimento),
                ValorTotal = ValorTotal,
            };
        }
    }
}
