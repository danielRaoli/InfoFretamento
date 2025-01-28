using Microsoft.AspNetCore.Identity;

namespace InfoFretamento.Domain.Entities
{
    public class Manutencao
    {

        public int Id { get; set; }
        public DateOnly DataLancamento { get; set; } 
        public DateOnly DataPrevista { get; set; }
        public DateOnly? DataRealizada { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
        public int KmPrevista { get; set; }
        public int KmAtual { get; set; }
        public int KmRealizada { get; set; }
        public decimal Custo { get; set; }
        public string TipoPagamento { get; set; } = string.Empty;
        public int Parcelas { get; set; }
        public bool Realizada { get; set; }
    }
}
