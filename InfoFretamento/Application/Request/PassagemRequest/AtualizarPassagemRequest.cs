using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PassagemRequest
{
    public record AtualizarPassagemRequest : BaseAtualizarRequest<Passagem>
    {
        public int PassageiroId { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public int Poltrona { get; set; }
        public string Situacao { get; set; } = string.Empty;
        public string EmailPassageiro { get; set; } = string.Empty;
        public string TelefonePassageiro { get; set; } = string.Empty;
        public string CpfPassageiro { get; set; } = string.Empty;
        public string NomePassageiro { get; set; } = string.Empty;
        public override Passagem UpdateEntity(Passagem entity)
        {
            entity.PassageiroId = PassageiroId;
            entity.FormaPagamento = FormaPagamento;
            entity.Poltrona = Poltrona;
            entity.Situacao = Situacao;
            entity.CpfPassageiro = CpfPassageiro;
            entity.EmailPassageiro = EmailPassageiro;
            entity.TelefonePassageiro = TelefonePassageiro;
            entity.NomePassageiro = NomePassageiro;
            return entity;
        }
    }
}
