using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.GastosMensais
{
    public record AtualizarSalarioRequest : BaseAtualizarRequest<Salario>
    {
        public DateTime DataVale { get; set; }
        public DateTime DataSalario { get; set; }
        public decimal ValorTotal { get; set; }

        public override Salario UpdateEntity(Salario entity)
        {
            entity.DataSalario = DateOnly.FromDateTime(DataSalario);
            entity.DataVale = DateOnly.FromDateTime(DataVale);
            entity.ValorTotal = ValorTotal;

            return entity;

        }
    }
}
