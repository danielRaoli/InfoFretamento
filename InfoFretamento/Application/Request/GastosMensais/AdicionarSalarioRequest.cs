using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.GastosMensais
{
    public class AdicionarSalarioRequest : IBaseAdicionarRequest<Salario>
    {

        public DateTime DataVale { get; set; }
        public DateTime DataSalario { get; set; }
        public decimal ValorTotal { get; set; }
        public int ResponsavelId { get; set; }


        public Salario ToEntity()
        {
            return new Salario
            {
                DataSalario = DateOnly.FromDateTime(DataSalario),
                DataVale = DateOnly.FromDateTime(DataVale),
                ValorTotal = ValorTotal,
                ResponsavelId = ResponsavelId
            };
        }
    }
}
