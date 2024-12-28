namespace InfoFretamento.Domain.Entities
{
    public class Colaborador : Pessoa
    {
        public DateOnly InicioFerias { get; set; }
        public DateOnly FimFerias{ get; set; }
    }
}
