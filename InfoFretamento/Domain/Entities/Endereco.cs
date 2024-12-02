using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.Entities
{
    [Owned]
    public class Endereco
    {
        public string Uf { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Rua { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
    }
}
