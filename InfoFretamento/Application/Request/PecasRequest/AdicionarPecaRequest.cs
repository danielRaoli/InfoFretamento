using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PecasRequest
{
    public class AdicionarPecaRequest : IBaseAdicionarRequest<AdicionarPeca>
    {
        public int PecaId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoTotal { get; set; }

        public AdicionarPeca ToEntity()
        {
           return new AdicionarPeca { PecaId = PecaId, Quantidade = Quantidade, PrecoTotal = PrecoTotal };
        }
    }
}
