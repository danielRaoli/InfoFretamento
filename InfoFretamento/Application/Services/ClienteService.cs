using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class ClienteService : IBaseService<Cliente, AdicionarClienteRequest, AtualizarClienteRequest>
    {
        private readonly IBaseRepository<Cliente> _repository;
        public async Task<Response<Cliente?>> AddAsync(AdicionarClienteRequest createRequest)
        {
            var entity = createRequest.ToEntity();
            var result = await _repository.AddAsync(entity);
            if (!result)
            {
                return new Response<Cliente?>(null, 500, "Erro ao tentar adicionar cliente, se o erro persistir consulte a manuntenção");
            }

            return new Response<Cliente?>(entity, 201, "Cliente Adicionado com sucesso");
        }

        public async Task<Response<List<Cliente>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return new Response<List<Cliente>>(result.ToList());
        }

       public async Task<Response<Cliente?>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if(entity is null)
            {
                return new Response<Cliente?>(null, 404, "Cliente nao encontrado");
            }
            return new Response<Cliente?>(entity, 200);
        }

       public async Task<Response<Cliente?>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if( entity is null)
            {
                return new Response<Cliente?>(null, 404, "Cliente nao encontrado");
            }

            var result = await _repository.DeleteAsync(entity);

            return result ? new Response<Cliente?>(null, 200, "Cliente Removido Com Sucesso") : new Response<Cliente?>(null, 500, "Nao foi possivel remover o cliente no momento");
        }

        public async Task<Response<Cliente?>> UpdateAsync(AtualizarClienteRequest updateRequest)
        {
            var entity = await _repository.GetByIdAsync(updateRequest.Id);
            if(entity is null)
            {
                return new Response<Cliente?>(null, 404, "Cliente nao encontrado");
            }

            entity.Cpf = updateRequest.Cpf; 
            entity.Endereco = updateRequest.Endereco;
            entity.DataNascimento = updateRequest.DataNascimento;
            entity.Nome = updateRequest.Nome;
            entity.Telefone = updateRequest.Telefone;   
            entity.Documento = updateRequest.Documento; 
            entity.TipoPessoa = updateRequest.Tipo;   
            
            var result = await _repository.UpdateAsync(entity);

            return result ? new Response<Cliente?>(null, 200, "Cliente Atualizado Com Sucesso") : new Response<Cliente?>(null, 500, "Nao foi possivel atualizar o cliente no momento");

        }
    }
}
