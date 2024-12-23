using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PassagemRequest
{
    public record AtualizarPassagemRequest : BaseAtualizarRequest<Passagem>
    {
        public int ViagemId { get; set; }
        public int PassageiroId { get; set; }
        public DateTime DataEmissao { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public int Poltrona { get; set; }
        public string Situacao { get; set; } = string.Empty;
        public override Passagem UpdateEntity(Passagem entity)
        {
            entity.ViagemId = ViagemId;
            entity.PassageiroId = PassageiroId;
            entity.DataEmissao = DataEmissao;
            entity.FormaPagamento = FormaPagamento;
            entity.Poltrona = Poltrona;
            entity.Situacao = Situacao;

            return entity;
        }
    }
}
