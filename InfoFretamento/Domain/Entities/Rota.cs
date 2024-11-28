using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.Entities
{
    [Owned]
    public class Rota
    {
        public Local Saida { get; set; }
        public Local Retorno { get; set; }
        
    }
}
