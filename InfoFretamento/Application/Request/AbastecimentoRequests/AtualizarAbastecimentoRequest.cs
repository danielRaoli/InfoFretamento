using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.AbastecimentoRequests
{
    public record AtualizarAbastecimentoRequest : BaseAtualizarRequest<Abastecimento>
    {
        public decimal ValorTotal { get; set; }
        public int Litros { get; set; }


        public override Abastecimento UpdateEntity(Abastecimento entity)
        {

            entity.Litros = Litros;
            entity.ValorTotal = ValorTotal;
            return entity;
        }
    }
}
