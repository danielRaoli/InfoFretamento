using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public record AtualizarGrupoCustoRequest  : BaseAtualizarRequest<GrupoDeCusto>
    {
        public string Nome { get; set; }

        public override GrupoDeCusto UpdateEntity(GrupoDeCusto entity)
        {
            entity.Nome = Nome;
            return entity;
        }
    }
}
