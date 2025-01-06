using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.FeriasRequest
{
    public record AtualizarFeriasRequest : BaseAtualizarRequest<Ferias>
    {
        public DateTime InicioFerias { get; set; }
        public DateTime FimFerias { get; set; }

        public override Ferias UpdateEntity(Ferias entity)
        {
            entity.InicioFerias = DateOnly.FromDateTime(InicioFerias);
            entity.FimFerias = DateOnly.FromDateTime(FimFerias);
            return entity;
        }
    }
}
