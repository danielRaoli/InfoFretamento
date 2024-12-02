namespace InfoFretamento.Domain.Entities
{
    public class Veiculo
    {
        public int Id { get; set; }
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

    }
}
