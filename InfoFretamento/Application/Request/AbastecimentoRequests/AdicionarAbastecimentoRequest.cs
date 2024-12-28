using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.AbastecimentoRequests
{
    public class AdicionarAbastecimentoRequest : IBaseAdicionarRequest<Abastecimento>
    {
        public decimal ValorTotal { get; set; }
        public int Litros { get; set; }
        public string CodigoNfe { get; set; } = string.Empty;
        public int ViagemId { get; set; }

        public Abastecimento ToEntity()
        {
            return new Abastecimento { ValorTotal = this.ValorTotal, Litros = this.Litros,CodigoNfe = this.CodigoNfe , ViagemId= ViagemId};
        }
    }
}
