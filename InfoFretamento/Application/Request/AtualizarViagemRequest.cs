using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AtualizarViagemRequest : BaseAtualizarRequest<Viagem>
    {
        public Rota Rota { get; set; }
        public DateTime DataSaida { get; set; }
        public string HorarioSaida { get; set; } = string.Empty;
        public DateTime DataRetorno { get; set; }
        public string HorarioRetorno { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string TipoServico { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int VeiculoId { get; set; }

        public override Viagem UpdateEntity(Viagem entity)
        {
            entity.Rota = Rota;
            entity.DataSaida = DataSaida;
            entity.HorarioSaida = HorarioSaida;
            entity.DataRetorno = DataRetorno;
            entity.HorarioRetorno = HorarioRetorno;
            entity.ClienteId = ClienteId;
            entity.TipoServico = TipoServico;
            entity.Status = Status;
            entity.VeiculoId = VeiculoId;   

            return entity;
        }
    }
}
