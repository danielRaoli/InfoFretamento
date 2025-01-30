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

        public async Task<decimal> MonthlyExpenses()
        {
            var currentDate = DateTime.Now;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;

            var abastecimentos = await _context.Abastecimentos.AsNoTracking().Include(a => a.Viagem)
                        .Where(a => a.Viagem.DataHorarioSaida.Data.Month == currentMonth
                            && a.Viagem.DataHorarioSaida.Data.Year == currentYear).Select(a => a.ValorTotal).SumAsync();
            var adiantamentos = await _context.Adiantamentos.Select(a => a.ValorDeAcerto).SumAsync();

            var despesasMensais = await _context.DespesasMensais.Select(d => d.ValorTotal).SumAsync();

            var salarios = await _context.Salarios.Select(s => s.ValorTotal).SumAsync();

           var despesas = await _context.Despesas
                .AsNoTracking()
                .Select(e =>
                    // Check if the expense is of type "Boleto"
                    e.FormaPagamento == "Boleto"
                        // Sum Boleto values with due dates in the current month/year
                        ? e.Boletos
                            .Where(b => b.Vencimento.Month == currentMonth &&
                                        b.Vencimento.Year == currentYear)
                            .Sum(b => b.Valor)
                        // For non-Boleto expenses, sum payments in the current month/year
                        : e.Pagamentos
                            .Where(p => p.DataPagamento.Month == currentMonth &&
                                        p.DataPagamento.Year == currentYear)
                            .Sum(p => p.ValorPago)
                )
                .SumAsync(); // Total sum of all filtered values

            return (abastecimentos + adiantamentos + despesasMensais + salarios + despesas);
        }



        public async Task<decimal> ReceitasMensais()
        {
            var currentDate = DateTime.Now;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;
            return await _context.Pagamentos.AsNoTracking().Where(r => r.DataPagamento.Month == _mesAtual && r.DataPagamento.Year == currentYear).SumAsync(d => d.ValorPago);
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
