

using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PassagemRequest
{
    public record AdicionarPassagemRequest : IBaseAdicionarRequest<Passagem>
    {
        public int ViagemId { get; set; }
        public DateTime DataEmissao { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public int Poltrona { get; set; }
        public string Situacao { get; set; } = string.Empty;
        public string EmailPassageiro { get; set; } = string.Empty;
        public string TelefonePassageiro { get; set; } = string.Empty;
        public string CpfPassageiro { get; set; } = string.Empty;
        public string NomePassageiro { get; set; } = string.Empty;
        public Passagem ToEntity()
        {
            return new Passagem
            {
                ViagemId = ViagemId,
                DataEmissao = DataEmissao,
                FormaPagamento = FormaPagamento,
                Poltrona = Poltrona,
                Situacao = Situacao,
                EmailPassageiro = EmailPassageiro,
                TelefonePassageiro = TelefonePassageiro,
                CpfPassageiro = CpfPassageiro,
                NomePassageiro = NomePassageiro,
            };
        }
    }
}
