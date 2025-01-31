using InfoFretamento.Application.Request.Base;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Request.GastosMensais
{
    public record AtualizarSalarioRequest : BaseAtualizarRequest<Salario>
    {
        public int DiaVale { get; set; }
        public int  DiaSalario { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorValeet { get; set; }

        public override Salario UpdateEntity(Salario entity)
        {
            entity.DiaSalario = DiaSalario;
            entity.DiaVale = DiaVale;
            entity.ValorTotal = ValorTotal;
            entity.ValorVale = ValorValeet; 
            return entity;

        }
    }
}
