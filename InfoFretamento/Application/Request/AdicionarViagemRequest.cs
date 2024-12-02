using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AdicionarViagemRequest : IBaseAdicionarRequest<Viagem>
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

        public Viagem ToEntity()
        {
            return new Viagem
            {
                Rota = this.Rota,
                DataSaida = this.DataSaida,
                HorarioSaida = this.HorarioSaida,
                DataRetorno = this.DataRetorno,
                HorarioRetorno = this.HorarioRetorno,
                ClienteId = this.ClienteId,
                TipoServico = this.TipoServico,
                VeiculoId = this.VeiculoId,
                Status = this.Status,

            };
        }
    }
}
