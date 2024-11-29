namespace InfoFretamento.Application.Request
{

    public record AtualizarColaboradorRequest : BasePessoaRequest
    {
        public int Id { get; set; }
    }

}
