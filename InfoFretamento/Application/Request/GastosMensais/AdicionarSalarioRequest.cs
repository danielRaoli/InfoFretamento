using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.GastosMensais
{
    public class AdicionarSalarioRequest : IBaseAdicionarRequest<Salario>
    {

        public int DiaVale { get; set; }
        public int DiaSalario  { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorVale { get; set; }
        public int ResponsavelId { get; set; }


        public Salario ToEntity()
        {
            return new Salario
            {
                DiaSalario = DiaSalario,
                DiaVale = DiaVale,
                ValorTotal = ValorTotal,
                ResponsavelId = ResponsavelId,
                ValorVale = ValorVale
            };
        }
    }
}
