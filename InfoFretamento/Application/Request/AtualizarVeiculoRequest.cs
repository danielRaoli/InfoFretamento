using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request
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
            entity.Ano = this.Ano;
            entity.Tipo = this.Tipo;
            entity.Prefixo = this.Prefixo;
            entity.KmAtual = this.KmAtual;
            entity.Placa = this.Placa;
            entity.Marca = this.Marca;
            entity.CapacidadeTank = this.CapacidadeTank;
            entity.Uf = this.Uf;
            entity.Carroceria = this.Carroceria;
            entity.QuantidadePoltronas = this.QuantidadePoltronas;
            entity.LocalEmplacado = this.LocalEmplacado;
            entity.Modelo = this.Modelo;    
            return entity;
        }
    }
}
