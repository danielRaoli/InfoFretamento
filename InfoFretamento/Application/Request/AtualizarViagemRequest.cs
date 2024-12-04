using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request
{
    public record AtualizarViagemRequest : BaseAtualizarRequest<Viagem>
    {
        public Rota Rota { get; set; }
        public Horario DataHorarioSaida { get; set; }
        public Horario DataHorarioRetorno { get; set; }
        public Horario DataHorarioSaidaGaragem { get; set; }
        public Horario DataHorarioChegada { get; set; }
        public int ClienteId { get; set; }
        public string TipoServico { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int VeiculoId { get; set; }

        public override Viagem UpdateEntity(Viagem entity)
        {
            entity.Rota = Rota;
            entity.DataHorarioSaida = DataHorarioSaida;
            entity.DataHorarioChegada = DataHorarioChegada;
            entity.DataHorarioRetorno = DataHorarioRetorno;
            entity.DataHorarioSaidaGaragem = DataHorarioSaidaGaragem;
            entity.ClienteId = ClienteId;
            entity.TipoServico = TipoServico;
            entity.Status = Status;
            entity.VeiculoId = VeiculoId;   

            return entity;
        }
    }
}
