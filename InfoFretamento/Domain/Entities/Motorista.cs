using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Domain.Entities
{
    public class Motorista : Pessoa
    {

        public List<Ferias> Ferias { get; set; } = [];
        public DateOnly DataAdmissao { get; set; }
        public List<MotoristaViagem> MotoristaViagens { get; set; } = [];
        public Habilitacao Habilitacao { get; set; }
        public List<Viagem> Viagens { get; set; } = [];

    }
}
