namespace InfoFretamento.Domain.Entities
{
    public class Boleto
    {
        public int Id { get; set; }
        public string Referencia { get; set; } = string.Empty;
        public DateOnly DataEmissao { get; set; }
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public int DespesaId { get; set; }
        public Despesa Despesa { get; set; }
        public bool Pago  { get; set; }
        public DateOnly Vencimento { get; set; }
        public DateOnly? DataPagamento { get; set; }


    }
}
