namespace InfoFretamento.Domain.Entities
{
    public class Cliente : Pessoa
    {
        public string TipoPessoa { get; set; } = string.Empty;
        public List<Viagem> Viagens { get; set; } = [];
    }
}
