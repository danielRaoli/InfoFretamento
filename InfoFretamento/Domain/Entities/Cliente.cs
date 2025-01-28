namespace InfoFretamento.Domain.Entities
{
    public class Cliente : Pessoa
    {
        public string NomeFantasia { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public List<Viagem> Viagens { get; set; } = [];

    }
}
