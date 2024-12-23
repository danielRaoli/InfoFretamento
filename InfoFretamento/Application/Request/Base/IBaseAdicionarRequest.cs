namespace InfoFretamento.Application.Request.Base
{
    public interface IBaseAdicionarRequest<TEntity> where TEntity : class
    {
        public TEntity ToEntity();
    }
}
