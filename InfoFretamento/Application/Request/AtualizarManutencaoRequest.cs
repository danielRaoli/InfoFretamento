using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AtualizarManutencaoRequest : BaseAtualizarRequest<Manutencao>
    {
        public DateTime DataLancamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataRealizada { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int ServicoId { get; set; }
        public int VeiculoId { get; set; }
        public int KmPrevista { get; set; }
        public int KmAtual { get; set; }
        public int KmRealizada { get; set; }
        public decimal Custo { get; set; }
        public override Manutencao UpdateEntity(Manutencao entity)
        {
            entity.DataLancamento = DataLancamento;
            entity.DataVencimento = DataVencimento;
            entity.DataRealizada = DataRealizada;
            entity.Tipo = Tipo;
            entity.ServicoId = ServicoId;
            entity.VeiculoId = VeiculoId;
            entity.KmPrevista = KmPrevista;
            entity.KmAtual = KmAtual;
            entity.KmRealizada = KmRealizada;
            entity.Custo = Custo;

            return entity;
        }
    }
}
