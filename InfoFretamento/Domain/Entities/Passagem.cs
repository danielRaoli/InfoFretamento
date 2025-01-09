namespace InfoFretamento.Domain.Entities
{
    public class Passagem
    {
        public int Id { get; set; }
        public int ViagemId { get; set; }
        public ViagemProgramada Viagem { get; set; }
        public string EmailPassageiro { get; set; } = string.Empty;
        public string TelefonePassageiro { get; set; } = string.Empty;
        public string CpfPassageiro  { get; set; } = string.Empty;
        public string NomePassageiro { get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public int Poltrona { get; set; }
        public string Situacao { get; set; } = string.Empty;
    }
}
