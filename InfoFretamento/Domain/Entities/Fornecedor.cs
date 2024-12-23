﻿namespace InfoFretamento.Domain.Entities
{
    public class Fornecedor : Pessoa
    {
        public string Tipo { get; set; } = string.Empty;
        public List<Despesa> Despesas { get; set; } = [];
        public List<Receita> Receitas { get; set; } = [];
    }
}
