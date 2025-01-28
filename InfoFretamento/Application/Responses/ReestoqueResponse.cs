using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Responses
{
    public class ReestoqueResponse
    {
        public AdicionarPeca AdicionarPeca { get; set; }
        public Despesa? Despesa { get; set; }
    }
}
