using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.FeriasRequest
{
    public class AdicionarFeriasRequest : IBaseAdicionarRequest<Ferias>
    {
        public DateTime InicioFerias { get; set; }
        public DateTime FimFerias { get; set; }
        public int ResponsavelId { get; set; }

        public Ferias ToEntity()
        {
            return new Ferias { ResponsavelId = ResponsavelId, InicioFerias =DateOnly.FromDateTime(InicioFerias), FimFerias = DateOnly.FromDateTime(FimFerias) };
        }
    }
}
