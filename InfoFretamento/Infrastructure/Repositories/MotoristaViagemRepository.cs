using InfoFretamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class MotoristaViagemRepository 
    {
        private readonly AppDbContext _context;

        public MotoristaViagemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<MotoristaViagem> motoristaViagens)
        {
            var existingKeys = await _context.Set<MotoristaViagem>()
                   .Select(mv => new { mv.MotoristaId, mv.ViagemId })
                   .ToListAsync();

            var newMotoristasViagens = motoristaViagens
                .Where(mv => !existingKeys.Any(e => e.MotoristaId == mv.MotoristaId && e.ViagemId == mv.ViagemId))
                .ToList();

            if (newMotoristasViagens.Any())
            {
                await _context.Set<MotoristaViagem>().AddRangeAsync(newMotoristasViagens);
                await _context.SaveChangesAsync();
            }

        }

        public void RemoveRangeAsync(IEnumerable<MotoristaViagem> motoristaViagens)
        {
            _context.Set<MotoristaViagem>().RemoveRange(motoristaViagens);

           
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
