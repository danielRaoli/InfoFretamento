namespace InfoFretamento.Domain.Entities
{
    public class Receita
    {
        public int Id { get; set; }
        public DateOnly DataCompra { get; set; }
        public string OrigemPagamento { get; set; } = string.Empty; //motorista, cliente, fornecedor 
        public string NumeroDocumento { get; set; } = string.Empty;
        public int ViagemId { get; set; }
        public Viagem Viagem { get; set; }
        public DateOnly Vencimento { get; set; }
        public bool Pago => ValorTotal == ValorPago;
        public decimal ValorTotal { get; set; }
        public decimal ValorPago =>  Pagamentos?.Sum(p => p.ValorPago) ?? 0;
        public List<Pagamento> Pagamentos { get; set; } = [];
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;
    }
}
