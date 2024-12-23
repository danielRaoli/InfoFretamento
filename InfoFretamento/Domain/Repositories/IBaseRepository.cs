using System.Linq.Expressions;

namespace InfoFretamento.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T?> GetWithFilterAsync(int id, params string[] includes);
        Task<IEnumerable<T>> GetAllWithFilterAsync(IEnumerable<Expression<Func<T, bool>>>? filters = null, params string[] includes);
    }
}
