using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.ManutencaoRequest;

public record AdicionarManutencaoRequest : IBaseAdicionarRequest<Manutencao>
{
    public DateTime DataPrevista { get; set; }
    public DateTime? DataRealizada { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int ServicoId { get; set; }
    public int VeiculoId { get; set; }
    public int KmPrevista { get; set; }
    public int KmAtual { get; set; }
    public int KmRealizada { get; set; }
    public decimal Custo { get; set; }
    public string? TipoPagamento { get; set; } = string.Empty;
    public int? Parcelas { get; set; }
    public List<DateTime> Vencimentos { get; set; } = [];
    public DateTime? VencimentoPagamento { get; set; }
    public bool Realizada { get; set; }

    public Manutencao ToEntity()
    {

        DateOnly? dataRealizada = DataRealizada is null ? null : DateOnly.FromDateTime(DataRealizada.Value);
        return new Manutencao
        {
            Realizada = Realizada,  
            DataLancamento = DateOnly.FromDateTime(DateTime.Now),
            DataPrevista = DateOnly.FromDateTime(DataPrevista),
            DataRealizada =  dataRealizada,
            Tipo = Tipo,
            ServicoId = ServicoId,
            VeiculoId = VeiculoId,
            KmPrevista = KmPrevista,
            KmAtual = KmAtual,
            KmRealizada = KmRealizada,
            Custo = Custo,
            Parcelas = Parcelas ?? 0,
            TipoPagamento = TipoPagamento ?? ""
        };
    }
}
