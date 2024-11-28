namespace InfoFretamento.Domain.Entities
{
    public class Viagem
    {
        public int Id { get; set; }
        public Rota Rota { get; set; }
        public DateTime DataSaida { get; set; }
        public string HorarioSaida { get; set; } = string.Empty;
        public DateTime DataRetorno { get; set; }
        public string HorarioRetorno { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public string TipoServico { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}
