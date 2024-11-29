using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class MotoristaService(IBaseRepository<Motorista> repository) : IBaseService<Motorista, AdicionarMotoristaRequest, AtualizarMotoristaRequest>
    {
        private readonly IBaseRepository<Motorista> _repository = repository;
        public async Task<Response<Motorista?>> AddAsync(AdicionarMotoristaRequest createRequest)
        {
            var entity = createRequest.ToEntity();
            var result = await _repository.AddAsync(entity);
            if (!result)
            {
                return new Response<Motorista?>(null, 500, "Erro ao tentar adicionar Motorista, se o erro persistir consulte a manuntenção");
            }

            return new Response<Motorista?>(entity, 201, "Motorista adicionado com sucesso");
        }

        public async Task<Response<List<Motorista>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return new Response<List<Motorista>>(result.ToList());
        }

        public async Task<Response<Motorista?>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
            {
                return new Response<Motorista?>(null, 404, "Motorista nao encontrado");
            }
            return new Response<Motorista?>(entity, 200);
        }

        public async Task<Response<Motorista?>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
            {
                return new Response<Motorista?>(null, 404, "Motorista nao encontrado");
            }

            var result = await _repository.DeleteAsync(entity);

            return result ? new Response<Motorista?>(null, 200, "Motorista Removido Com Sucesso") : new Response<Motorista?>(null, 500, "Nao foi possivel remover o motorista no momento");
        }

        public async Task<Response<Motorista?>> UpdateAsync(AtualizarMotoristaRequest updateRequest)
        {
            var entity = await _repository.GetByIdAsync(updateRequest.Id);
            if (entity is null)
            {
                return new Response<Motorista?>(null, 404, "Motorista nao encontrado");
            }

            entity.Cpf = updateRequest.Cpf;
            entity.Endereco = updateRequest.Endereco;
            entity.DataNascimento = updateRequest.DataNascimento;
            entity.Nome = updateRequest.Nome;
            entity.Telefone = updateRequest.Telefone;
            entity.Documento = updateRequest.Documento;
            entity.Habilitacao = updateRequest.Habilitacao;

            var result = await _repository.UpdateAsync(entity);

            return result ? new Response<Motorista?>(null, 200, "Motorista Atualizado Com Sucesso") : new Response<Motorista?>(null, 500, "Nao foi possivel atualizar o motorista no momento");

        }
    }
}
