using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.ValueObjects
{

    public class Rota
    {
        public Local Saida { get; set; }
        public Local Retorno { get; set; }

    }
}
