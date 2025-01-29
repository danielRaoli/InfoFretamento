namespace InfoFretamento.Domain.Entities
{
    public class Colaborador : Pessoa
    {
        public List<Ferias> Ferias { get; set; } = [];

    }
}
