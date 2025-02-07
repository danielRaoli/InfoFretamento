using InfoFretamento.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace InfoFretamento.Domain.Entities
{
    public class Motorista : Pessoa
    {

        public List<Ferias> Ferias { get; set; } = [];
        public DateOnly DataAdmissao { get; set; }
        [JsonIgnore]
        public List<MotoristaViagem> MotoristaViagens { get; set; } = [];
        public Habilitacao Habilitacao { get; set; }
        public List<Viagem> Viagens { get; set; } = [];

    }
}
