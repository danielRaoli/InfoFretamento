using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
{
    public class AdicionarVeiculoRequest : IBaseAdicionarRequest<Veiculo>
    {
        public string Prefixo { get; set; } = string.Empty;
        public int KmAtual { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string LocalEmplacado { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Carroceria { get; set; } = string.Empty;
        public int CapacidadeTank { get; set; }
        public int Ano { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int QuantidadePoltronas { get; set; }
        public List<Despesa> Despesas { get; set; } = [];

        public Veiculo ToEntity()
        {
            return new Veiculo
            {
                Ano = this.Ano,
                Tipo = this.Tipo,
                Prefixo = this.Prefixo,
                KmAtual = this.KmAtual,
                Placa = this.Placa,
                Marca = this.Marca,
                CapacidadeTank = this.CapacidadeTank,
                Uf = this.Uf,
                Carroceria = this.Carroceria,
                QuantidadePoltronas = this.QuantidadePoltronas,
                LocalEmplacado = this.LocalEmplacado
            };
        }
    }
}
