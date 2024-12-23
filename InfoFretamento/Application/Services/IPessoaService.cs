using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Application.Services
{
    public interface IPessoaService<T> where T : Pessoa
    {
        public Task<Response<List<T>>> GetAllNameContains(string name = null);
    }
}
