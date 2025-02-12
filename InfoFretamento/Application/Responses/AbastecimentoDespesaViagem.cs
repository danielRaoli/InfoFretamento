using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Responses
{
    public record AbastecimentoDespesaViagem
    {
        public Abastecimento Abastecimento { get; set; }
        public Despesa Despesa { get; set; }
    }
}
