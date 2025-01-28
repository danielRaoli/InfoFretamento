namespace InfoFretamento.Domain.Entities
{
    public class Salario
    {
        public int Id { get; set; }
        public DateOnly DataVale { get; set; }
        public DateOnly DataSalario { get; set; }
        public decimal ValorTotal { get; set; }
        public int ResponsavelId { get; set; }
        public Pessoa Responsavel { get; set; }

    }
}
