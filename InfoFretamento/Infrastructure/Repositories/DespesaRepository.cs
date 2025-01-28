using InfoFretamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class DespesaRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Despesa>> GetByEntityId(int entityId, string entityType )
        {
            var despesa = await _context.Despesas.AsNoTracking().Include(d => d.Pagamentos).Include(d => d.Boletos).Where(d => d.EntidadeId == entityId  && d.EntidadeOrigem == entityType).ToListAsync();
            return despesa;
        }

      

        public async Task<bool> DeleteAsync(int entityId, string entityType)
        {
            var despesas = await GetByEntityId(entityId, entityType);
            _context.Despesas.RemoveRange(despesas);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
