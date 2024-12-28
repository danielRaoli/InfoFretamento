using InfoFretamento.Application.Request.Auth;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Repositories;


namespace InfoFretamento.Application.Services
{
    public class AuthService(IUserRepository repositorio, ITokenService tokenService) 
    {
        private readonly IUserRepository _userRepository = repositorio;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<Response<string?>> Login(LoginRequest request)
        {
            var usuarioValido = await _userRepository.Login(request.UserName, request.Password);
            if (!usuarioValido)
            {
                throw new Exception("Usuario nao encontrado");
            }

            var token = _tokenService.GenerateToken(request.UserName);

            return new Response<string?>(token);
        }



    }
}
