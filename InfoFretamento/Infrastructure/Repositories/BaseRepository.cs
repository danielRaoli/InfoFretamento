using InfoFretamento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
    {

        private readonly AppDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public async Task<bool> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            var result = await _context.SaveChangesAsync(); 
            return result > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entityList = await _dbSet.ToListAsync();
            return entityList;  
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
           var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
