

using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.PassagemRequest
{
    public record AdicionarPassagemRequest : IBaseAdicionarRequest<Passagem>
    {
        public int ViagemId { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public int? PoltronaIda { get; set; }
        public int? PoltronaVolta { get; set; }
        public string Situacao { get; set; } = string.Empty;
        public string EmailPassageiro { get; set; } = string.Empty;
        public string TelefonePassageiro { get; set; } = string.Empty;
        public string CpfPassageiro { get; set; } = string.Empty;
        public string NomePassageiro { get; set; } = string.Empty;
        public string Tipo  { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public decimal ValorPersonalizado { get; set; }
        public string CidadePassageiro { get; set; } = string.Empty;
        public string ParadaPassageiro { get; set; } = string.Empty;
        public Passagem ToEntity()
        {
            return new Passagem
            {
                ViagemId = ViagemId,
                DataEmissao = DateOnly.FromDateTime(DateTime.Now),
                FormaPagamento = FormaPagamento,
                PoltronaIda = PoltronaIda,
                PoltronaVolta = PoltronaVolta,
                Situacao = Situacao,
                EmailPassageiro = EmailPassageiro,
                TelefonePassageiro = TelefonePassageiro,
                CpfPassageiro = CpfPassageiro,
                NomePassageiro = NomePassageiro,
                CidadePassageiro = CidadePassageiro,
                Tipo = Tipo,
                ValorTotal = ValorTotal,
            };
        }
    }
}
