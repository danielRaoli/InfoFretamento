﻿using InfoFretamento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

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

        public async Task<T?> GetByIdAsync(int id )
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
           var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<T?> GetWithFilterAsync(
            int id,
            params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Adiciona os includes dinâmicos (incluindo relacionamentos aninhados)
            foreach (var include in includes)
            {
                query = query.Include(include);
            }


            // Busca por ID
            query = query.Where(e => EF.Property<int>(e, "Id") == id);

            // Executa a query e retorna o resultado
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T?>> GetAllWithFilterAsync(IEnumerable<Expression<Func<T, bool>>>? filters = null, params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Adiciona os includes dinâmicos (incluindo relacionamentos aninhados)
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }


            // Executa a query e retorna o resultado
            return await query.ToListAsync();
        }
    }
}
