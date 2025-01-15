using InfoFretamento.Application.Request.Base;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public abstract class BaseService<TEntity, TAdicionarDto, TAtualizarDto> : IBaseService<TEntity, TAdicionarDto, TAtualizarDto>
        where TEntity : class
        where TAdicionarDto : IBaseAdicionarRequest<TEntity>
        where TAtualizarDto : BaseAtualizarRequest<TEntity>
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IMemoryCache _cache;
        private readonly CacheManager _cacheManager;

        protected BaseService(IBaseRepository<TEntity> repository, IMemoryCache cache, CacheManager cacheManager)
        {
            _repository = repository;
            _cache = cache;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Adiciona um novo item e limpa o cache.
        /// </summary>
        public virtual async Task<Response<TEntity?>> AddAsync(TAdicionarDto createRequest)
        {
            var entity = createRequest.ToEntity();
            var result = await _repository.AddAsync(entity);

            if (result)
            {
                _cacheManager.ClearAll($"{typeof(TEntity).Name}");
                return new Response<TEntity?>(entity);
            }

            return new Response<TEntity?>(null, 500, "Erro ao tentar adicionar novo item");
        }

        /// <summary>
        /// Obtém todos os itens com cache.
        /// </summary>
        public async Task<Response<List<TEntity>>> GetAllAsync()
        {
            var cacheKey = $"{typeof(TEntity).Name}_GetAll";

            var data = await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                _cacheManager.AddKey(cacheKey);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);

                var result = await _repository.GetAllAsync();
                return result.ToList();
            });

           

            return new Response<List<TEntity>>(data);
        }

        /// <summary>
        /// Obtém um item por ID com cache.
        /// </summary>
        public async Task<Response<TEntity?>> GetByIdAsync(int id)
        {

            var entity =  await _repository.GetByIdAsync(id);

            return entity is null
                ? new Response<TEntity?>(null, 404, "O item procurado não existe")
                : new Response<TEntity?>(entity);
        }

        /// <summary>
        /// Remove um item e limpa o cache.
        /// </summary>
        public async Task<Response<TEntity?>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
            {
                return new Response<TEntity?>(null, 404, "O item procurado não existe");
            }

            var result = await _repository.DeleteAsync(entity);

            if (result)
            {
                _cacheManager.ClearAll($"{typeof(TEntity).Name}"); 
                return new Response<TEntity?>(entity);
            }

            return new Response<TEntity?>(null, 500, "Erro ao tentar remover o item");
        }

        /// <summary>
        /// Atualiza um item e limpa o cache.
        /// </summary>
        public virtual async Task<Response<TEntity?>> UpdateAsync(TAtualizarDto updateRequest)
        {
            var entity = await _repository.GetByIdAsync(updateRequest.Id);
            if (entity is null)
            {
                return new Response<TEntity?>(null, 404, "O item procurado não existe");
            }

            entity = updateRequest.UpdateEntity(entity);
            var result = await _repository.UpdateAsync(entity);

            if (result)
            {
                _cacheManager.ClearAll($"{typeof(TEntity).Name}");
                return new Response<TEntity?>(entity);
            }

            return new Response<TEntity?>(null, 500, "Erro ao tentar atualizar o item");
        }
    }
}