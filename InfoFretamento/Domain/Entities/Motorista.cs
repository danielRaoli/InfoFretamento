namespace InfoFretamento.Domain.Entities
{
    public class Motorista : Pessoa
    {

     
        public Habilitacao Habilitacao { get; set; }
        public List<Viagem> Viagens { get; set; } = [];
    }
}
