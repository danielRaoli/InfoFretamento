using InfoFretamento.Application.Request.Base;
using InfoFretamento.Application.Request.MotoristaRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public abstract class BasePessoaService<TEntity, TAdicionarDto, TAtualizarDto> : IPessoaService<TEntity>, IBaseService<TEntity, TAdicionarDto, TAtualizarDto> where TEntity : Pessoa where TAdicionarDto : IBaseAdicionarRequest<TEntity> where TAtualizarDto : BaseAtualizarRequest<TEntity>
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IPessoaRepository<TEntity> _pessoaRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly CacheManager _cacheManager;    
        public BasePessoaService(IBaseRepository<TEntity> repository, IPessoaRepository<TEntity> pessoaRepository, IMemoryCache memoryCache, CacheManager cacheManager)
        {
            _repository = repository;
            _pessoaRepository = pessoaRepository;
            _memoryCache = memoryCache;
            _cacheManager = cacheManager;
        }

        public async Task<Response<TEntity?>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? new Response<TEntity?>(null, 404, "O item procurado nao existe") : new Response<TEntity?>(entity);
        }

        public async Task<Response<List<TEntity>>> GetAllAsync()
        {

            var cacheKey = $"{typeof(TEntity).Name}_All";
            var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                _cacheManager.AddKey(cacheKey);
                return await _repository.GetAllAsync();
            });
            
            return new Response<List<TEntity>>(result.ToList());
        }

        public async Task<Response<List<TEntity>>> GetAllNameContains(string name = null)
        {

            var cachekey = $"{typeof(TEntity).Name}_{name ?? "null"}";
            var result = await _memoryCache.GetOrCreateAsync(cachekey, async entry =>
            {
                if (name != null)
                {
                    return await _pessoaRepository.GetAllNameContains(name);
                }


                return await _repository.GetAllAsync();
            });
            




            return new Response<List<TEntity>>(result.ToList());
        }

        public async Task<Response<TEntity?>> AddAsync(TAdicionarDto createRequest)
        {
            var entity = createRequest.ToEntity();
            var result = await _repository.AddAsync(entity);
            if (!result) { new Response<TEntity?>(null, 500, "Erro ao tentar adicionar novo item"); }

            _cacheManager.ClearAll($"{typeof(TEntity).Name}");
            return new Response<TEntity?>(entity);
        }




        public async Task<Response<TEntity?>> RemoveAsync(int id)
        {

            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) { return new Response<TEntity?>(null, 404, "O item procurado nao existe"); }

            var result = await _repository.DeleteAsync(entity);

            if (!result) { new Response<TEntity?>(null, 500, "Erro ao tentar remvoer novo item"); }
            _cacheManager.ClearAll($"{typeof(TEntity).Name}");
            return  new Response<TEntity?>(entity);

        }

        public async Task<Response<TEntity?>> UpdateAsync(TAtualizarDto updateRequest)
        {

            var entity = await _repository.GetByIdAsync(updateRequest.Id);
            if (entity is null) { return new Response<TEntity?>(null, 404, "O item procurado nao existe"); }

            entity = updateRequest.UpdateEntity(entity);

            var result = await _repository.UpdateAsync(entity);
            if (!result) { new Response<TEntity?>(null, 500, "Erro ao tentar atualizar novo item"); }

            _cacheManager.ClearAll($"{typeof(TEntity).Name}");

            return  new Response<TEntity?>(entity);

        }
    }
}
