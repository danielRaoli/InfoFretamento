using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Responses
{
    public class ManutencaoResponse
    {
        public Manutencao Manutencao { get; set; }
        public Despesa? Despesa  { get; set; }
    }
}
