using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Domain.Entities
{
    public class Viagem
    {
        public int Id { get; set; }
        public Rota Rota { get; set; }
        public Horario DataHorarioSaida { get; set; }
        public Horario DataHorarioRetorno { get; set; }
        public Horario DataHorarioSaidaGaragem { get; set; }
        public Horario DataHorarioChegada { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } 
        public string TipoServico { get; set; } = string.Empty;
        public string TipoViagem { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<MotoristaViagem> MotoristaViagens { get; set; } = [];
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public string TipoPagamento { get; set; } = string.Empty;
        public int Parcelas { get; set; }
        public decimal ValorContratado { get; set; }
        public decimal ValorDespesas { get; set; }
        public string Itinerario { get; set; } = string.Empty;

        public List<Abastecimento> Abastecimentos { get; set; }
        public Adiantamento Adiantamento { get; set; }
        public int KmInicialVeiculo { get; set; }
        public int KmFinalVeiculo { get; set; }
        public Receita Receita { get; set; }
        public decimal ValorPago => Receita?.ValorPago ?? 0 ;
    }
}
