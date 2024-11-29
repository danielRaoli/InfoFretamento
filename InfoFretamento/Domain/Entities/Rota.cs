using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.Entities
{

    public class Rota
    {
        public Local Saida { get; set; }
        public Local Retorno { get; set; }
        
    }
}
