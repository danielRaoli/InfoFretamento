using InfoFretamento.Application.Request.ClienteRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class ClienteService(IBaseRepository<Cliente> repository, IPessoaRepository<Cliente> pessoaRepository) : BasePessoaService<Cliente, AdicionarClienteRequest, AtualizarClienteRequest>(repository, pessoaRepository)
    {

        private readonly IBaseRepository<Cliente> _repository = repository;


        public async Task<Response<Cliente?>> GetWithIncludes(int id)
        {

            var response = await _repository.GetWithFilterAsync(id, new string[] { "Despesas", "Receitas","Viagens" });
            return response != null ? new Response<Cliente?>(response) : new Response<Cliente?>(null, 404, "motorista não encontrado");
        }
    }
}
