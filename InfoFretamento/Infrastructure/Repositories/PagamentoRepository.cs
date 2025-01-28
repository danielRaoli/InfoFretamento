using InfoFretamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class PagamentoRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<PagamentoDespesa?> GetById(int id)
        {
            var entity = await _context.PagamentosDespesa.FirstOrDefaultAsync(p => p.Id == id);
            return entity;
        }
        public async Task<bool> RemovePagamento(PagamentoDespesa pagamento)
        {
            _context.PagamentosDespesa.Remove(pagamento);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> AdicionarPagamento(PagamentoDespesa pagamento)
        {
            await _context.PagamentosDespesa.AddAsync(pagamento);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
