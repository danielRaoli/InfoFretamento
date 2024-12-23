using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class PessoaService<T>(IPessoaRepository<T> repository) : IPessoaService<T> where T : Pessoa
    {
        private readonly IPessoaRepository<T> _repository = repository;

        public async Task<Response<List<T>>> GetAllNameContains(string name)
        {
           var listPessoa = await _repository.GetAllNameContains(name);
            return new Response<List<T>>(listPessoa.ToList());
        }
    }
}
