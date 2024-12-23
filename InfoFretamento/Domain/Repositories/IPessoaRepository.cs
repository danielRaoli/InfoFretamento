using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Domain.Repositories
{
    public interface IPessoaRepository<T> where T : Pessoa
    {
        Task<IEnumerable<T>> GetAllNameContains(string name = null);
    }
}
