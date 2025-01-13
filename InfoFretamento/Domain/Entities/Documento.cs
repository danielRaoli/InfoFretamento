namespace InfoFretamento.Domain.Entities
{
    public class Documento
    {
        public int Id { get; set; }
        public DateOnly Vencimento { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public bool Pendente { get; set; } 
    }
}
