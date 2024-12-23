using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class PessoaRepository<T>(AppDbContext context) : IPessoaRepository<T> where T : Pessoa
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<IEnumerable<T>> GetAllNameContains(string name = null)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Nome.Contains(name));

            return await query.ToListAsync();
        }
    }
}
