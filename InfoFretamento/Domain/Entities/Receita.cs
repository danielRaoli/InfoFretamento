namespace InfoFretamento.Domain.Entities
{
    public class Receita
    {
        public int Id { get; set; }
        public DateOnly DataEmissao { get; set; }
        public DateOnly DataCompra { get; set; }
        public string OrigemPagamento { get; set; } = string.Empty; //motorista, cliente, fornecedor 
        public string NumeroDocumento { get; set; } = string.Empty;
        public int ResponsavelId { get; set; }
        public Pessoa Responsavel { get; set; }
        public int ViagemId { get; set; }
        public Viagem Viagem { get; set; }
        public DateOnly Vencimento { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorParcial { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string CentroCusto { get; set; } = string.Empty;
    }
}
