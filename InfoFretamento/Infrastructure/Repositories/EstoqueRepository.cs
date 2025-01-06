using InfoFretamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class EstoqueRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<RetiradaPeca?> AddRetiradaAsync(RetiradaPeca entity)
        {
            _context.Retiradas.Add(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? entity : null;
        }

        public async Task<AdicionarPeca?> AddPecaEstoque(AdicionarPeca entity)
        {
            _context.Adicionamentos.Add(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? entity : null;
        }

        public async Task<List<RetiradaPeca>> GetAllRetiradas(DateOnly minDate, DateOnly maxDate, string? veiculo = null)
        {
            IQueryable<RetiradaPeca>  query = _context.Retiradas;

            query = query.Include(r => r.Veiculo).Include(r => r.Peca);
            if (veiculo != null)
            {
                query.Where(r => r.Veiculo.Prefixo.Equals(veiculo));
            }

            query.Where(r => r.DataDeRetirada >= minDate && r.DataDeRetirada <= maxDate);
            return await query.ToListAsync();


        }


        public async Task<RetiradaPeca?> GetRetiradaById(int id)
        {
            var retirada = await _context.Retiradas.FirstOrDefaultAsync(r => r.Id == id);
            return retirada;
        }

        public async Task<AdicionarPeca?> GetAdicionamentoById(int id)
        {
            var adicionamento = await _context.Adicionamentos.FirstOrDefaultAsync(r => r.Id == id);
            return adicionamento;
        }


        public async Task<List<AdicionarPeca>> GetAllAdicionamentos(DateOnly minDate, DateOnly maxDate)
        {
            var adicionamentos = await _context.Adicionamentos.Include(a => a.Peca).Where(a => a.DataDeEntrada >= minDate && a.DataDeEntrada <= maxDate).ToListAsync();
            return adicionamentos;
        }

        public async Task<bool> RemoveRetirada(RetiradaPeca retirada)
        {
            _context.Retiradas.Remove(retirada);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> RemoveAdicionamento(AdicionarPeca adicionamento)
        {
            _context.Adicionamentos.Remove(adicionamento);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AtualizarEstoqueAsync(Peca peca)
        {
            _context.Set<Peca>().Update(peca);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
