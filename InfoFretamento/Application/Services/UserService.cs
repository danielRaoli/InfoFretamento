using InfoFretamento.Application.Request.Auth;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using System.Data;


namespace InfoFretamento.Application.Services
{
    public class AuthService( ITokenService tokenService) 
    {
        private readonly List<User> _users = new List<User> { new User {UserName = "Marcelo", Password = "senha123", Role = "admin" }, new User{ UserName = "Passagem", Password = "passagem123", Role = "passagem" } };
        private readonly ITokenService _tokenService = tokenService;
        public Response<string?> Login(LoginRequest request)
        {
            var usuarioValido = _users.FirstOrDefault(u => u.UserName.Equals(request.UserName) && u.Password.Equals(request.Password));
            if (usuarioValido is null)
            {
                throw new Exception("Usuario nao encontrado");
            }

            var token = _tokenService.GenerateToken(request.UserName, usuarioValido.Role);

            return new Response<string?>(token);
        }



    }
}
