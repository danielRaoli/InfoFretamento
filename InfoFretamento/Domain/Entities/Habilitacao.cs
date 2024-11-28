﻿namespace InfoFretamento.Domain.Entities
{
    public class Habilitacao
    {
        public int Id { get; set; }
        public string Protocolo { get; set; } = string.Empty;
        public DateTime Vencimento { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
    }
}
