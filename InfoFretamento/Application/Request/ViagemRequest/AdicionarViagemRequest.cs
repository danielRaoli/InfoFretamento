using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request.ViagemRequest;

public record AdicionarViagemRequest : IBaseAdicionarRequest<Viagem>
{
    public Rota Rota { get; set; }
    public HorarioRequest DataHorarioSaida { get; set; }
    public HorarioRequest DataHorarioRetorno { get; set; }
    public HorarioRequest DataHorarioSaidaGaragem { get; set; }
    public HorarioRequest DataHorarioChegada { get; set; }
    public int ClienteId { get; set; }
    public string TipoServico { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string TipoViagem { get; set; } = string.Empty;
    public string TipoPagamento { get; set; } = string.Empty;
    public decimal ValorContratado { get; set; }
    public string Itinerario { get; set; } = string.Empty;
    public int KmFinalVeiculo { get; set; }
    public int KmInicialVeiculo { get; set; }
    public int VeiculoId { get; set; }
    public List<int> MotoristasId { get; set; } = [];

    public Viagem ToEntity()
    {
        return new Viagem
        {
            Rota = Rota,
            DataHorarioSaida = DataHorarioSaida.toEntity(),
            DataHorarioSaidaGaragem = DataHorarioSaidaGaragem.toEntity(),
            DataHorarioRetorno = DataHorarioRetorno.toEntity(),
            DataHorarioChegada = DataHorarioChegada.toEntity(),
            ClienteId = ClienteId,
            TipoServico = TipoServico,
            VeiculoId = VeiculoId,
            Status = Status,
            TipoViagem = TipoViagem,
            TipoPagamento = TipoPagamento,
            ValorContratado = ValorContratado,
            Itinerario = Itinerario,
            MotoristaViagens = MotoristasId.Select(motoristaId => new MotoristaViagem
            {
                MotoristaId = motoristaId
            }).ToList(),
            KmFinalVeiculo = KmFinalVeiculo,
            KmInicialVeiculo= KmInicialVeiculo

        };
    }
}
public record HorarioRequest
{
    public DateTime Data { get; set; }
    public string Hora { get; set; } = string.Empty;

    public Horario toEntity()
    {
        return new Horario { Data = DateOnly.FromDateTime(Data), Hora = Hora };
    }
}
