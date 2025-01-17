using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PagamentosRequest
{
    public record AtualizarPagamentoRequest : BaseAtualizarRequest<Pagamento>
    {
        public override Pagamento UpdateEntity(Pagamento entity)
        {
            throw new NotImplementedException();
        }
    }
}
