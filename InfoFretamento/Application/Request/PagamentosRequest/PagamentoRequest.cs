using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record PagamentoRequest : IBaseAdicionarRequest<Pagamento>
    {
        public int ReceitaId  { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
        public Pagamento ToEntity()
        {
            return new Pagamento { ReceitaId = ReceitaId, ValorPago = ValorPago, DataPagamento = DateOnly.FromDateTime(DataPagamento) };
        }
    }
}
