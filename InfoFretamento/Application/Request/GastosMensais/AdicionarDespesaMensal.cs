using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.GastosMensais
{
    public class AdicionarDespesaMensal : IBaseAdicionarRequest<DespesaMensal>
    {
        public int  DiaPagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public string CentroDeCusto { get; set; } = string.Empty;

        public DespesaMensal ToEntity()
        {
           return new DespesaMensal { CentroDeCusto = CentroDeCusto, ValorTotal = ValorTotal, DiaPagamento = DiaPagamento};
        }
    }
}
