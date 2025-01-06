namespace InfoFretamento.Domain.Entities
{
    public class Ferias
    {
        public int Id { get; set; }
        public int ResponsavelId { get; set; }
        public Pessoa Responsavel { get; set; }
        public DateOnly InicioFerias { get; set; }
        public DateOnly FimFerias { get; set; }
    }
}
