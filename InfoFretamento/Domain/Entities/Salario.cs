namespace InfoFretamento.Domain.Entities
{
    public class Salario
    {
        public int Id { get; set; }
        public int DiaVale { get; set; }
        public int DiaSalario { get; set; }
        public decimal ValorVale { get; set; }
        public decimal ValorTotal { get; set; }
        public int ResponsavelId { get; set; }
        public Pessoa Responsavel { get; set; }

    }
}
