using InfoFretamento.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class DashBoardRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        private static int _mesAtual = DateTime.Now.Month;
        public async Task<int> TotalViagens()
        {
          
            return await _context.Viagens.AsNoTracking().Where(v => v.DataHorarioSaida.Data.Month == _mesAtual).CountAsync();
        }

        public async Task<decimal> DespesasMensais()
        {
            return await _context.Despesas.AsNoTracking().Where(d => d.DataCompra.Month == _mesAtual).SumAsync(d => d.ValorTotal);
        }


        public async Task<decimal> ReceitasMensais()
        {
            return await _context.Receitas.AsNoTracking().Where(r => r.DataCompra.Month == _mesAtual).SumAsync(d => d.ValorTotal);
        }

        public async Task<List<ReceitasMensais>> ValorLiquidoMensal(int ano)
        {
            var receitasMensais = await _context.Receitas
               .Where(r => r.DataCompra.Year == ano) // Filtra receitas pelo ano
               .GroupBy(r => r.DataCompra.Month)    // Agrupa por mês
               .Select(g => new
               {
                   Month = g.Key,
                   Total = g.Sum(r => r.ValorTotal) // Soma os valores das receitas no mês
               })
               .ToListAsync();

                    var despesasMensais = await _context.Despesas
                        .Where(d => d.DataCompra.Year == ano) // Filtra despesas pelo ano
                        .GroupBy(d => d.DataCompra.Month)    // Agrupa por mês
                        .Select(g => new
                        {
                            Month = g.Key,
                            Total = g.Sum(d => d.ValorTotal) // Soma os valores das despesas no mês
                        })
                .ToListAsync();

                // Combina receitas e despesas para cada mês
                var resumoMensal = Enumerable.Range(1, 12).Select(mes =>
                {
                    var receitas = receitasMensais.FirstOrDefault(r => r.Month == mes)?.Total ?? 0;
                    var despesas = despesasMensais.FirstOrDefault(d => d.Month == mes)?.Total ?? 0;

                    return new ReceitasMensais
                    {
                        Month = new DateTime(ano, mes, 1).ToString("MMM", new System.Globalization.CultureInfo("pt-BR")), // Nome reduzido do mês
                        Receitas = receitas,
                        Despesas = despesas,
                        ValorLiquido = receitas - despesas
                    };
                }).ToList();

            return resumoMensal;
        }

    }
}
