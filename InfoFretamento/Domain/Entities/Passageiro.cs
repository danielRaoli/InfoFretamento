namespace InfoFretamento.Domain.Entities
{
    public class Passageiro : Pessoa
    {
        public string Cartao { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public List<Passagem> Passagens { get; set; } = [];
    }
}
