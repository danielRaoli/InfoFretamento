using InfoFretamento.Application.Request.Base;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public abstract class BaseService<TEntity, TAdicionarDto, TAtualizarDto> : IBaseService<TEntity, TAdicionarDto, TAtualizarDto> where TEntity : class where TAdicionarDto : IBaseAdicionarRequest<TEntity> where TAtualizarDto : BaseAtualizarRequest<TEntity>
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IMemoryCache _memoryCache;
        public BaseService(IBaseRepository<TEntity> repository, IMemoryCache cache)
        {
            _repository = repository;
            _memoryCache = cache;
        }
        public async Task<Response<TEntity?>> AddAsync(TAdicionarDto createRequest)
        {
            var entity = createRequest.ToEntity();
            var result = await _repository.AddAsync(entity);
            if (result)
            {
                _memoryCache.Remove($"{typeof(TEntity).Name}_All"); // Remove o cache da lista
                return new Response<TEntity?>(entity);
            }
            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar adicionar novo item");
        }

        public async Task<Response<List<TEntity>>> GetAllAsync()
        {
            string cacheKey = $"{typeof(TEntity).Name}_All";

            var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Configura a expiração do cache
                var data = await _repository.GetAllAsync();
                return data.ToList();
            });
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

            if (result)
            {
                _memoryCache.Remove($"{typeof(TEntity).Name}_*"); // Remove o cache da lista
                return new Response<TEntity?>(entity);
            }

            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar remvoer novo item");

        }

        public async Task<Response<TEntity?>> UpdateAsync(TAtualizarDto updateRequest)
        {
            var entity = await _repository.GetByIdAsync(updateRequest.Id);
            if (entity is null) { return new Response<TEntity?>(null, 404, "O item procurado nao existe"); }

            entity = updateRequest.UpdateEntity(entity);

            var result = await _repository.UpdateAsync(entity);
            if (result)
            {
                _memoryCache.Remove($"{typeof(TEntity).Name}_*"); // Remove o cache da lista
                return new Response<TEntity?>(entity);
            }

            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar atualizar novo item");

        }
    }
}
