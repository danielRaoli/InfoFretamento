namespace InfoFretamento.Domain.Entities
{
    public class DocumentoCliente
    {
        public int Id { get; set; }
        public string Documento { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int ClienteId { get; set; }
    }
}
