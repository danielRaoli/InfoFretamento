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
            var currentDate = DateTime.UtcNow.AddHours(-3);
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;

            var abastecimentos = await _context.Abastecimentos.AsNoTracking()
                        .Where(a => a.DataPagamento.Month == currentMonth
                            && a.DataPagamento.Year == currentYear).Select(a => a.ValorTotal).SumAsync();
            var adiantamentos = await _context.Adiantamentos.AsNoTracking().Include(a => a.Viagem).Where(a => a.Viagem.DataHorarioSaida.Data.Month == currentMonth
                                         && a.Viagem.DataHorarioSaida.Data.Year == currentYear).Select(a => a.ValorDeAcerto).SumAsync();

            var despesasMensais = await _context.DespesaMensal.AsNoTracking().Select(d => d.ValorTotal).SumAsync();

            var salarios = await _context.Salario.AsNoTracking().Select(s => s.ValorTotal).SumAsync();

            var despesas = await _context.Despesas
                 .AsNoTracking()
                 .Select(e =>
                     // Check if the expense is of type "Boleto"
                     e.FormaPagamento == "Boleto"
                         // Sum Boleto values with due dates in the current month/year
                         ? e.Boletos
                             .Where(b => b.Pago == true && b.DataPagamento.Value.Month == currentMonth &&
                                         b.DataPagamento.Value.Year == currentYear)
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
            var currentDate = DateTime.UtcNow.AddHours(-3);
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;

            var receitasViagem = await _context.Pagamentos.AsNoTracking().Where(r => r.DataPagamento.Month == _mesAtual && r.DataPagamento.Year == currentYear).SumAsync(d => d.ValorPago);
            var receitaPacotes = await _context.Passagens.AsNoTracking().Include(p => p.Viagem).Where(p => p.Viagem.Saida.Data.Month == currentMonth && p.Viagem.Saida.Data.Year == currentYear).SumAsync(p => p.ValorTotal);

            return receitaPacotes + receitasViagem;
        }

        public async Task<List<ReceitasMensais>> ValorLiquidoMensal(int ano)
        {
            var receitasMensais = await _context.Pagamentos.AsNoTracking()
               .Where(r => r.DataPagamento.Year == ano) // Filtra receitas pelo ano
               .GroupBy(r => r.DataPagamento.Month)    // Agrupa por mês
               .Select(g => new
               {
                   Month = g.Key,
                   Total = g.Sum(r => r.ValorPago) // Soma os valores das receitas no mês
               })
               .ToListAsync();

            var receitasViagem = await _context.Passagens.AsNoTracking().Include(p => p.Viagem)
               .Where(p => p.Viagem.Saida.Data.Year == ano) // Filtra receitas pelo ano
               .GroupBy(p => p.Viagem.Saida.Data.Month)    // Agrupa por mês
               .Select(g => new
               {
                   Month = g.Key,
                   Total = g.Sum(p => p.ValorTotal) // Soma os valores das receitas no mês
               })
               .ToListAsync();

            var despesasMensais = await _context.PagamentosDespesa
                        .Where(d => d.DataPagamento.Year == ano) // Filtra despesas pelo ano
                        .GroupBy(d => d.DataPagamento.Month)    // Agrupa por mês
                        .Select(g => new
                        {
                            Month = g.Key,
                            Total = g.Sum(d => d.ValorPago) // Soma os valores das despesas no mês
                        })
                .ToListAsync();

            var boletosDespesas = await _context.Boletos
            .Where(d => d.Pago == true && d.DataPagamento.Value.Year == ano) // Filtra despesas pelo ano
            .GroupBy(d => d.DataPagamento.Value.Month)    // Agrupa por mês
            .Select(g => new
            {
                Month = g.Key,
                Total = g.Sum(d => d.Valor) // Soma os valores das despesas no mês
            })
            .ToListAsync();

            var salarios = await _context.Salario.AsNoTracking().Select(s => s.ValorTotal).SumAsync();
            var desepsasaFixas = await _context.DespesaMensal.AsNoTracking().Select(d => d.ValorTotal).SumAsync();
            // Combina receitas e despesas para cada mês
            var resumoMensal = Enumerable.Range(1, 12).Select(mes =>
                {
                    var receitas = receitasMensais.FirstOrDefault(r => r.Month == mes)?.Total ?? 0;
                    var receitasPassagens = receitasViagem.FirstOrDefault(r => r.Month == mes)?.Total ?? 0;

                    var despesas = despesasMensais.FirstOrDefault(d => d.Month == mes)?.Total ?? 0;
                    var boletos = boletosDespesas.FirstOrDefault(b => b.Month == mes)?.Total ?? 0;

                    return new ReceitasMensais
                    {
                        Month = new DateTime(ano, mes, 1).ToString("MMM", new System.Globalization.CultureInfo("pt-BR")), // Nome reduzido do mês
                        Receitas = receitas + receitasPassagens,
                        Despesas = despesas + boletos +salarios +desepsasaFixas,     
                        ValorLiquido = receitas - despesas
                    };
                }).ToList();

            return resumoMensal;
        }

    }
}
