using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public abstract record BaseAtualizarRequest<TEntity> 
    {
        public int Id { get; set; }

        public abstract TEntity UpdateEntity(TEntity entity);

    }
}
