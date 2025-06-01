using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Domain.Entities
{
    public class ViagemProgramada
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public HorarioLocal Saida { get; set; }
        public HorarioLocal Retorno { get; set; }
        public HorarioLocal Chegada { get; set; }
        public decimal ValorPassagem { get; set; }
        public decimal ValorPassagemIdaVolta { get; set; }
        public string FormaPagto { get; set; } = string.Empty;
        public string Responsavel { get; set; } = string.Empty;
        public string Guia { get; set; } = string.Empty;
        public string Itinerario  { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public List<Passagem> Passagens { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
