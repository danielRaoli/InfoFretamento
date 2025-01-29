using InfoFretamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class BoletoRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> AddRange(IEnumerable<Boleto> boletos)
        {
           await _context.AddRangeAsync(boletos);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Boleto?> GetById(int id)
        {
            var entity = await _context.Boletos.FirstOrDefaultAsync(b => b.Id == id);
            return entity;
         
        }

        public async Task<bool> Pagar(Boleto boleto)
        {
             _context.Boletos.Update(boleto);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
