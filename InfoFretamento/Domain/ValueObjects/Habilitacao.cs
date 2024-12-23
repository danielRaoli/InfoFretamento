using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.ValueObjects
{
    [Owned]
    public class Habilitacao
    {

        public string Protocolo { get; set; } = string.Empty;
        public DateOnly Vencimento { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;

    }
}
