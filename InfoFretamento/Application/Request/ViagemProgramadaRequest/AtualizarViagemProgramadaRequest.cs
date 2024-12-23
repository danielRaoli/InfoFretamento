using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Application.Request.ViagemProgramadaRequest
{
    public record AtualizarViagemProgramadaRequest : BaseAtualizarRequest<ViagemProgramada>
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
        public int VeiculoId { get; set; }

        public override ViagemProgramada UpdateEntity(ViagemProgramada entity)
        {
            entity.Titulo = Titulo; // Atualiza o título
            entity.Descricao = Descricao; // Atualiza a descrição
            entity.Saida = Saida; // Atualiza a saída
            entity.Retorno = Retorno; // Atualiza o retorno
            entity.Chegada = Chegada; // Atualiza a chegada
            entity.ValorPassagem = ValorPassagem; // Atualiza o valor da passagem
            entity.FormaPagto = FormaPagto; // Atualiza a forma de pagamento
            entity.Responsavel = Responsavel; // Atualiza o responsável
            entity.Guia = Guia; // Atualiza o guia
            entity.Itinerario = Itinerario; // Atualiza o itinerário
            entity.Observacoes = Observacoes; // Atualiza as observações
            entity.VeiculoId = VeiculoId; // Atualiza o ID do veículo

            return entity;
        }
    }
}
