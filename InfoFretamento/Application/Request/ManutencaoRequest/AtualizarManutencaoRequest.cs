using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.ManutencaoRequest
{
    public record AtualizarManutencaoRequest : BaseAtualizarRequest<Manutencao>
    {
        public DateTime DataPrevista { get; set; }
        public DateTime ? DataRealizada { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int KmPrevista { get; set; }
        public int KmAtual { get; set; }
        public int KmRealizada { get; set; }
        public bool Realizada { get; set; }
        public decimal Custo { get; set; }
        public string? TipoPagamento { get; set; } = string.Empty;
        public int? Parcelas { get; set; }
        public List<DateTime> Vencimentos { get; set; } = [];
        public DateTime? VencimentoPagamento { get; set; }

        public override Manutencao UpdateEntity(Manutencao entity)
        {
            DateOnly? dataRealizada = DataRealizada is null ? null : DateOnly.FromDateTime(DataRealizada.Value);
            entity.DataPrevista = DateOnly.FromDateTime( DataPrevista);
            entity.DataRealizada = dataRealizada;
            entity.Tipo = Tipo;
            entity.KmPrevista = KmPrevista;
            entity.KmAtual = KmAtual;
            entity.KmRealizada = KmRealizada;
            entity.Custo = Custo;
            entity.Realizada = Realizada;
            entity.TipoPagamento = TipoPagamento ?? "";
            entity.Parcelas = Parcelas ?? 0;
            

            return entity;
        }
    }
}
