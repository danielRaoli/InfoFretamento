using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AdicionarGrupoDeCustoRequest  : IBaseAdicionarRequest<GrupoDeCusto>
    {
       
        public string Nome { get; set; }

        public GrupoDeCusto ToEntity()
        {
           return new GrupoDeCusto { Nome = Nome };
        }
    }
}
