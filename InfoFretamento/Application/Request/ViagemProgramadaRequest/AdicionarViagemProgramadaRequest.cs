using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request.ViagemProgramadaRequest
{
    public record AdicionarViagemProgramadaRequest : IBaseAdicionarRequest<ViagemProgramada>
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public HorarioLocal Saida { get; set; }
        public HorarioLocal Retorno { get; set; }
        public HorarioLocal Chegada { get; set; }
        public decimal ValorPassagem { get; set; }
        public string FormaPagto { get; set; } = string.Empty;
        public string Responsavel { get; set; } = string.Empty;
        public string Guia { get; set; } = string.Empty;
        public string Itinerario { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public decimal ValorPassagemIdaVolta { get; set; }

        public int VeiculoId { get; set; }

        public ViagemProgramada ToEntity()
        {
            return new ViagemProgramada
            {
                Titulo = Titulo,
                Descricao = Descricao,
                Saida = Saida,
                Retorno = Retorno,
                Chegada = Chegada,
                ValorPassagem = ValorPassagem,
                FormaPagto = FormaPagto,
                Responsavel = Responsavel,
                Guia = Guia,
                Itinerario = Itinerario,
                Observacoes = Observacoes,
                VeiculoId = VeiculoId,
                ValorPassagemIdaVolta = ValorPassagemIdaVolta,  
                CreatedAt = DateTime.UtcNow.AddHours(-3) // Ajuste para o horário de Brasília   
            };
        }
    }
}
