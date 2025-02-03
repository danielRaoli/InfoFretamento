using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.AbastecimentoRequests
{
    public class AdicionarAbastecimentoRequest : IBaseAdicionarRequest<Abastecimento>
    {
        public decimal ValorTotal { get; set; }
        public int Litros { get; set; }
        public int ViagemId { get; set; }
        public DateTime DataPagamento { get; set; }

        public Abastecimento ToEntity()
        {
            return new Abastecimento { ValorTotal = this.ValorTotal, Litros = this.Litros , ViagemId= ViagemId, DataPagamento =DateOnly.FromDateTime(DataPagamento)};
        }
    }
}
