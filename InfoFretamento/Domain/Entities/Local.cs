using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.Entities
{
    [Owned]
    public class Local
    {

        public string UfSaida { get; set; } = string.Empty;
        public string CidadeSaida { get; set; } = string.Empty;
        public string CidadeDestino { get; set; } = string.Empty;
        public string LocalDeSaida { get; set; } = string.Empty;

    }
}
