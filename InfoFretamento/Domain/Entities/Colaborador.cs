namespace InfoFretamento.Domain.Entities
{
    public class Colaborador : Pessoa
    {
        public DateOnly DataAdmissao { get; set; }
        public List<Ferias> Ferias { get; set; } = [];

    }
}
