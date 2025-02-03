namespace InfoFretamento.Domain.Entities
{
    public class Despesa 
    {
        public int Id { get; set; }
        public DateOnly DataCompra { get; set; }
        public string EntidadeOrigem { get; set; } = string.Empty;
        public int EntidadeId { get; set; }
        public DateOnly? Vencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public List<PagamentoDespesa> Pagamentos { get; set; }
        public int Parcelas { get; set; }
        public List<Boleto> Boletos { get; set; } = [];
        public string CentroCusto { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal ValorParcial => Pagamentos?.Select(p => p.ValorPago).Sum() ?? 0 ;
        public bool Pago => ValorParcial == ValorTotal && FormaPagamento != "Boleto" || ParcelasPagas == Parcelas && FormaPagamento == "Boleto";
        public int ParcelasPagas => Boletos.Count(b => b.Pago);
        public bool BoletosPagos => ParcelasPagas == Parcelas;
    }
}
