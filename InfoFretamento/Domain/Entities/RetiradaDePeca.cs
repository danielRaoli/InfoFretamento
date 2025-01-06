namespace InfoFretamento.Domain.Entities
{
    public class RetiradaPeca
    {
        public int Id { get; set; }
        public int PecaId { get; set; }
        public Peca Peca { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo{ get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoTotal { get; set; }
        public DateOnly DataDeRetirada { get; set; }
    }
}
