using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public abstract class BaseService<TEntity, TAdicionarDto, TAtualizarDto> : IBaseService<TEntity, TAdicionarDto, TAtualizarDto> where TEntity : class where TAdicionarDto : IBaseAdicionarRequest<TEntity> where TAtualizarDto : BaseAtualizarRequest<TEntity>
    {
        private readonly IBaseRepository<TEntity> _repository;
        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task<Response<TEntity?>> AddAsync(TAdicionarDto createRequest)
        {
            var entity = createRequest.ToEntity();
            var result = await _repository.AddAsync(entity);

            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar adicionar novo item");
        }

        public async Task<Response<List<TEntity>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return new Response<List<TEntity>>(result.ToList());
        }

        public async Task<Response<TEntity?>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? new Response<TEntity?>(null, 404, "O item procurado nao existe") : new Response<TEntity?>(entity);
        }

        public async Task<Response<TEntity?>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) { return new Response<TEntity?>(null, 404, "O item procurado nao existe"); }

            var result = await _repository.DeleteAsync(entity);


            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar remvoer novo item");

        }

        public async Task<Response<TEntity?>> UpdateAsync(TAtualizarDto updateRequest)
        {
            var entity = await _repository.GetByIdAsync(updateRequest.Id);
            if (entity is null) { return new Response<TEntity?>(null, 404, "O item procurado nao existe"); }

            entity = updateRequest.UpdateEntity(entity);

            var result = await _repository.UpdateAsync(entity);


            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar atualizar novo item");

        }
    }
}
