namespace InfoFretamento.Application.Services
{
    public interface ITokenService
    {
        public string GenerateToken(string email, string role);
    }
}
