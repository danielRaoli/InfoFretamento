using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.ValueObjects
{
    [Owned]
    public record Horario
    {
        public DateTime Data { get; set; }
        public string Hora { get; set; } = string.Empty;
    }
}
