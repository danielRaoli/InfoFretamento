using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PecasRequest
{
    public class RegistrarPecaRequest : IBaseAdicionarRequest<Peca>
    {
        public int Quantidade { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }

        public Peca ToEntity()
        {
            return new Peca { Nome = Nome, Preco = Preco, Quantidade = Quantidade };
        }
    }
}
