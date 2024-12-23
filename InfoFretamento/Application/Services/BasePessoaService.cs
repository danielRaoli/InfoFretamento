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
        public BasePessoaService(IBaseRepository<TEntity> repository, IPessoaRepository<TEntity> pessoaRepository, IMemoryCache cache)
        {
            _repository = repository;
            _pessoaRepository = pessoaRepository;
            _memoryCache = cache;
        }
        public async Task<Response<TEntity?>> AddAsync(TAdicionarDto createRequest)
        {
            var entity = createRequest.ToEntity();
            var result = await _repository.AddAsync(entity);

            if (result)
            {
                _memoryCache.Remove($"{typeof(TEntity).Name}_*");
            }
            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar adicionar novo item");
        }

        public async Task<Response<List<TEntity>>> GetAllAsync()
        {
            var cashKey = $"{typeof(TEntity).Name}_All";
            var result = await _memoryCache.GetOrCreateAsync(cashKey, async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(120));
                var response = await _repository.GetAllAsync();
                return response;
            });
            

            return new Response<List<TEntity>>(result.ToList());
        }

        public async Task<Response<List<TEntity>>> GetAllNameContains(string name = null)
        {
            var cashKey = $"{typeof(TEntity).Name}_AllName";
            var result = await _memoryCache.GetOrCreateAsync(cashKey, async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(120));
                var response = await _pessoaRepository.GetAllNameContains(name);
                return response;
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
            var cacheKey = $"{typeof(TEntity).Name}_*";
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) { return new Response<TEntity?>(null, 404, "O item procurado nao existe"); }

            var result = await _repository.DeleteAsync(entity);
            if (result)
            {
                _memoryCache.Remove(cacheKey);
            }

            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar remvoer novo item");

        }

        public async Task<Response<TEntity?>> UpdateAsync(TAtualizarDto updateRequest)
        {
            var cacheKey = $"{typeof(TEntity).Name}_*";
            var entity = await _repository.GetByIdAsync(updateRequest.Id);
            if (entity is null) { return new Response<TEntity?>(null, 404, "O item procurado nao existe"); }

            entity = updateRequest.UpdateEntity(entity);

            var result = await _repository.UpdateAsync(entity);
            if (result) { 
            _memoryCache.Remove(cacheKey) ;
            }

            return result ? new Response<TEntity?>(entity) : new Response<TEntity?>(null, 500, "Erro ao tentar atualizar novo item");

        }
    }
}
