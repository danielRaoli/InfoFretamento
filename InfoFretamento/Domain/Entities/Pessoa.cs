using InfoFretamento.Domain.ValueObjects;

namespace InfoFretamento.Domain.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public DocumentoCliente Documento { get; set; }
        public Endereco Endereco { get; set; }
        public string Cpf { get; set; } = string.Empty;
    }
}
