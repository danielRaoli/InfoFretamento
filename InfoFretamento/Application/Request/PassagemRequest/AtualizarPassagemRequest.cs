using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PassagemRequest
{
    public record AtualizarPassagemRequest : BaseAtualizarRequest<Passagem>
    {

        public string FormaPagamento { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public string EmailPassageiro { get; set; } = string.Empty;
        public string TelefonePassageiro { get; set; } = string.Empty;
        public string CpfPassageiro { get; set; } = string.Empty;
        public string NomePassageiro { get; set; } = string.Empty;
        public string CidadePassageiro { get; set; } = string.Empty;
        public override Passagem UpdateEntity(Passagem entity)
        {

            entity.FormaPagamento = FormaPagamento;
            entity.Situacao = Situacao;
            entity.CpfPassageiro = CpfPassageiro;
            entity.EmailPassageiro = EmailPassageiro;
            entity.TelefonePassageiro = TelefonePassageiro;
            entity.NomePassageiro = NomePassageiro;
            entity.CidadePassageiro = CidadePassageiro;
            return entity;
        }
    }
}
