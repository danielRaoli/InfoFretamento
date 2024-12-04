using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request
{
    public record AdicionarViagemRequest : IBaseAdicionarRequest<Viagem>
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

        public Viagem ToEntity()
        {
            return new Viagem
            {
                Rota = this.Rota,
                DataHorarioSaida = this.DataHorarioSaida,
                DataHorarioSaidaGaragem = this.DataHorarioSaidaGaragem,
                DataHorarioRetorno = this.DataHorarioRetorno,
                DataHorarioChegada = this.DataHorarioChegada,
                ClienteId = this.ClienteId,
                TipoServico = this.TipoServico,
                VeiculoId = this.VeiculoId,
                Status = this.Status,

            };
        }
    }
}
