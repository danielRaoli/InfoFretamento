using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Domain.Entities
{
    public class Motorista : Pessoa
    {

        public DateOnly InicioFerias { get; set; }
        public DateOnly FimFerias { get; set; }
        public Habilitacao Habilitacao { get; set; }
        public List<Despesa> Despesas { get; set; } = [];
        public List<Receita> Receitas { get; set; } = [];
        public List<Viagem> Viagens { get; set; } = [];
    }
}
