using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.VeiculoRequest
{
    public record AtualizarVeiculoRequest : BaseAtualizarRequest<Veiculo>
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

        public override Veiculo UpdateEntity(Veiculo entity)
        {
            entity.Ano = Ano;
            entity.Tipo = Tipo;
            entity.Prefixo = Prefixo;
            entity.KmAtual = KmAtual;
            entity.Placa = Placa;
            entity.Marca = Marca;
            entity.CapacidadeTank = CapacidadeTank;
            entity.Uf = Uf;
            entity.Carroceria = Carroceria;
            entity.QuantidadePoltronas = QuantidadePoltronas;
            entity.LocalEmplacado = LocalEmplacado;
            entity.Modelo = Modelo;
            return entity;
        }
    }
}
