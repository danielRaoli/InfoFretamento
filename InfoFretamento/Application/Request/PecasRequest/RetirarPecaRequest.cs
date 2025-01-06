using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PecasRequest
{
    public class RetirarPecaRequest : IBaseAdicionarRequest<RetiradaPeca>
    {
        public int PecaId { get; set; }
        public int VeiculoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoTotal { get; set; }

        public RetiradaPeca ToEntity()
        {
           return  new RetiradaPeca { PecaId = PecaId, Quantidade = Quantidade, PrecoTotal = PrecoTotal, DataDeRetirada = DateOnly.FromDateTime(DateTime.Now), VeiculoId = VeiculoId };
        }
    }
}
