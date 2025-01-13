using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.VeiculoRequest
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
        public string Modelo { get; set; } = string.Empty;
        public int QuantidadePoltronas { get; set; }
        public string Acessorios { get; set; } = string.Empty;

        public Veiculo ToEntity()
        {
            return new Veiculo
            {
                Ano = Ano,
                Tipo = Tipo,
                Prefixo = Prefixo,
                KmAtual = KmAtual,
                Placa = Placa,
                Marca = Marca,
                CapacidadeTank = CapacidadeTank,
                Uf = Uf,
                Carroceria = Carroceria,
                QuantidadePoltronas = QuantidadePoltronas,
                LocalEmplacado = LocalEmplacado,
                Modelo = Modelo,
                Acessorios = Acessorios,
            };
        }
    }
}
