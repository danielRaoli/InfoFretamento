namespace InfoFretamento.Application.Request
{
    public interface IBaseAdicionarRequest<TEntity> where TEntity : class
    {
        public TEntity ToEntity();
    }
}
