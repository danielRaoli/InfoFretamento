namespace InfoFretamento.Domain.Entities
{
    public class Abastecimento 
    {
        public  int  Id { get; set; }
        public decimal ValorTotal { get; set; }
        public int Litros { get; set; }
        public int ViagemId { get; set; }
        public Viagem Viagem{ get; set; }
        
    }
}
