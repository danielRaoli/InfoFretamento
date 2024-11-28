namespace InfoFretamento.Domain.Entities
{
    public class Documento
    {
        public int Id { get; set; }
        public DateTime Vencimento { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
    }
}
