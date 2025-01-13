namespace InfoFretamento.Domain.Entities
{
    public class MotoristaViagem
    {
        public int MotoristaId { get; set; }
        public Motorista Motorista { get; set; }

        public int ViagemId { get; set; }
        public Viagem Viagem { get; set; }
    }
}
