using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PecasRequest
{
    public record EditarPecaRequest : BaseAtualizarRequest<Peca>
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }

        public override Peca UpdateEntity(Peca entity)
        {
            entity.Preco = Preco;
            entity.Nome = Nome; 
            return entity;
        }
    }
}
