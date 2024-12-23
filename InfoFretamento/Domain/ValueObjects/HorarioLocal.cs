using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.ValueObjects
{
    [Owned]
    public record HorarioLocal : Horario
    {
        public string Local { get; set; } = string.Empty;
    }
}
