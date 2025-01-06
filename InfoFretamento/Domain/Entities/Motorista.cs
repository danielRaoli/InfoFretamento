﻿using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Domain.Entities
{
    public class Motorista : Pessoa
    {

        public List<Ferias> Ferias { get; set; } = [];
        public Habilitacao Habilitacao { get; set; }
        public List<Despesa> Despesas { get; set; } = [];
        public List<Receita> Receitas { get; set; } = [];
        public List<Viagem> Viagens { get; set; } = [];
    }
}
