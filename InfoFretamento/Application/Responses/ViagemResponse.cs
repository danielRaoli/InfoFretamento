using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Domain.Entities
{
    public class ViagemResponse
    {
        public Viagem Viagem { get; set; }
        public List<Despesa> Despesas { get; set; }
        public decimal ValorPago => Viagem.Receita?.ValorPago ?? 0;
        public decimal TotalDespesa => Despesas?.Sum(d => d.ValorTotal) ?? 0;

        public decimal ValorLiquidoViagem => CalcularValorLiquido();



        private decimal CalcularValorLiquido()
        {
            var valorAbastecimento = Viagem.Abastecimentos.Select(a => a.ValorTotal).Sum();
            var valorAdiantamento = Viagem.Adiantamento?.ValorDeAcerto ?? 0;

            return Viagem.ValorContratado - valorAbastecimento - valorAdiantamento - TotalDespesa;
        }
    }
}
