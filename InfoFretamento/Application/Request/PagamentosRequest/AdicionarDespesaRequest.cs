using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record AdicionarDespesaRequest :  IBaseAdicionarRequest<Despesa>
    {
        public DateTime? Vencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string EntidadeOrigem { get; set; } = string.Empty; 

        public int Parcelas { get; set; }
        public List<DateTime> VencimentosBoleto { get; set; } = [];
        public int EntidadeId { get; set; }

        public Despesa ToEntity()
        {
            return new Despesa
            {
                DataCompra = DateOnly.FromDateTime(DateTime.Now),
                CentroCusto = CentroCusto,
                Vencimento = FormaPagamento == "Boleto" ? DateOnly.FromDateTime(VencimentosBoleto.FirstOrDefault()) : DateOnly.FromDateTime(Vencimento.Value),
                ValorTotal = ValorTotal,
                FormaPagamento = FormaPagamento,
                Descricao = Descricao,
                EntidadeOrigem = EntidadeOrigem,
                EntidadeId = EntidadeId,
                Parcelas = Parcelas,
               
            };
        }
    }
}
