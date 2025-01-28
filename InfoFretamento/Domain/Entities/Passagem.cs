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
        public DateOnly DataEmissao { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public int? PoltronaIda { get; set; }
        public int? PoltronaVolta { get; set; }
        public string Situacao { get; set; } = string.Empty;
        public string CidadePassageiro { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public string ParadaPassageiro { get; set; } = string.Empty;

    }
}
