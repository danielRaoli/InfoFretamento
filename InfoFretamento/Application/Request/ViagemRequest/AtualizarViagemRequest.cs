using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request.ViagemRequest
{
    public record AtualizarViagemRequest : BaseAtualizarRequest<Viagem>
    {
        public Rota Rota { get; set; }
        public int VeiculoId { get; set; }
        public List<int> MotoristasId { get; set; } = [];
        public Horario DataHorarioSaida { get; set; }
        public Horario DataHorarioRetorno { get; set; }
        public Horario DataHorarioSaidaGaragem { get; set; }
        public Horario DataHorarioChegada { get; set; }
        public string TipoServico { get; set; } = string.Empty;
        public string Itinerario { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int KmInicialVeiculo { get; set; }
        public int KmFinalVeiculo { get; set; }
        public decimal ValorParcial { get; set; }
        public decimal ValorContratado { get; set; }

        public override Viagem UpdateEntity(Viagem entity)
        {
            if(entity.Status == "PENDENTE")
            {
               
                entity.ValorContratado = ValorContratado;
            }
            entity.Rota = Rota;
            entity.DataHorarioSaida = DataHorarioSaida;
            entity.DataHorarioChegada = DataHorarioChegada;
            entity.DataHorarioRetorno = DataHorarioRetorno;
            entity.DataHorarioSaidaGaragem = DataHorarioSaidaGaragem;
            entity.TipoServico = TipoServico;
            entity.Status = Status;
            entity.KmFinalVeiculo = KmFinalVeiculo;
            entity.VeiculoId = VeiculoId;
            entity.KmInicialVeiculo = KmInicialVeiculo;
            entity.Itinerario = Itinerario;
            return entity;
        }
    }
}
