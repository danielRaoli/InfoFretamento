using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request;

public record AdicionarManutencaoRequest : IBaseAdicionarRequest<Manutencao>
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

    public Manutencao ToEntity()
    {
        return new Manutencao
        {
            DataLancamento = DataLancamento,
            DataVencimento = DataVencimento,
            DataRealizada = DataRealizada,
            Tipo = Tipo,
            ServicoId = ServicoId,
            VeiculoId = VeiculoId,
            KmPrevista = KmPrevista,
            KmAtual = KmAtual,
            KmRealizada = KmRealizada,
            Custo = Custo,

        };
    }
}
