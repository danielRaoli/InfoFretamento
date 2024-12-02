using InfoFretamento.Domain.Entities;
using System.Text.Json.Serialization;

namespace InfoFretamento.Application.Request
{
    public abstract record BaseAtualizarRequest<TEntity> 
    {
        [JsonIgnore]
        public int Id { get; set; }

        public abstract TEntity UpdateEntity(TEntity entity);

    }
}
