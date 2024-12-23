using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.ValueObjects
{
    [Owned]
    public class Local
    {

        public string UfSaida { get; set; } = string.Empty;
        public string CidadeSaida { get; set; } = string.Empty;
        public string LocalDeSaida { get; set; } = string.Empty;

    }
}
