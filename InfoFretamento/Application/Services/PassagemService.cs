using InfoFretamento.Application.Request.PassagemRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class PassagemService(IBaseRepository<Passagem> repository, IMemoryCache cache, CacheManager cacheManager, IBaseRepository<ViagemProgramada> viagemProgramadaRepository) : BaseService<Passagem, AdicionarPassagemRequest,AtualizarPassagemRequest>(repository, cache, cacheManager)
    {
        private readonly IBaseRepository<Passagem> _repository = repository;
        private readonly CacheManager _cacheManager = cacheManager; 
        private readonly IBaseRepository<ViagemProgramada> _viagemRepository = viagemProgramadaRepository;

        public async Task<Response<List<Passagem>>> GetAll(int viagemId)
        {
            var filters = new List<Expression<Func<Passagem, bool>>>();

            filters.Add(p => p.ViagemId == viagemId);

            var result = await _repository.GetAllWithFilterAsync(filters, ["Viagem", "Viagem.Veiculo"]);
            
            return new Response<List<Passagem>>(result.ToList());
        }

        public override async Task<Response<Passagem?>> AddAsync(AdicionarPassagemRequest createRequest)
        {
            var viagemDaPassagem = await _viagemRepository.GetByIdAsync(createRequest.ViagemId);
            if(viagemDaPassagem == null)
            {
                return new Response<Passagem?>(null, 404, "A viagem da passagem respctiva nao foi encontrada");
            }

            if (createRequest.Tipo.ToUpper().Equals("IDA"))
            {
                createRequest.ValorTotal = viagemDaPassagem.ValorPassagem;
            }
            else
            {
                createRequest.ValorTotal = viagemDaPassagem.ValorPassagemIdaVolta;
            }

           return  await base.AddAsync(createRequest);
        }

        public override async Task<Response<Passagem?>> UpdateAsync(AtualizarPassagemRequest updateRequest)
        {
            var entity = await _repository.GetWithFilterAsync(updateRequest.Id, "Viagem");
            if (entity == null)
            {
                return new Response<Passagem?>(null, 404, "Passagem nao foi encontrada");
            }

            if (entity.Viagem == null)
            {
                return new Response<Passagem?>(null, 404, "Viagem nao foi encontrada");
            }

            if (updateRequest.Tipo.ToUpper().Equals("IDA") && entity.Tipo.ToUpper() != "IDA" )
            {
                updateRequest.ValorTotal = entity.Viagem.ValorPassagem;
            }
            else if(updateRequest.Tipo.Equals("IDA-VOLTA") && entity.Tipo.ToUpper() != "IDA-VOLTA")
            {
                updateRequest.ValorTotal = entity.Viagem.ValorPassagemIdaVolta;
            }


            entity = updateRequest.UpdateEntity(entity);
            var result = await _repository.UpdateAsync(entity);

            if (result)
            {
                _cacheManager.ClearAll($"{typeof(Passagem).Name}");
                return new Response<Passagem?>(entity);
            }

            return new Response<Passagem?>(null, 500, "Erro ao tentar atualizar o item");
        }
    }
}
