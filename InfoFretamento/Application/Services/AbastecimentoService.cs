using InfoFretamento.Application.Request.AbastecimentoRequests;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class AbastecimentoService(IBaseRepository<Abastecimento> repository, IMemoryCache memoryCache, CacheManager cacheManager, DespesaRepository despesaRepository, IBaseRepository<Despesa> baseDespesaRepository) : BaseService<Abastecimento, AdicionarAbastecimentoRequest, AtualizarAbastecimentoRequest>(repository, memoryCache, cacheManager)
    {
        private readonly IBaseRepository<Abastecimento> _repository = repository;
        private readonly DespesaRepository _despesaRepository = despesaRepository;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly CacheManager _cacheManager = cacheManager;
        private readonly IBaseRepository<Despesa> _basedespesaRepository = baseDespesaRepository;
        public async Task<Response<AbastecimentoDespesaViagem?>> Add(AdicionarAbastecimentoRequest createRequest)
        {
            using var transaction = await _repository.BeginTransactionAsync();
            try
            {


                var entity = createRequest.ToEntity();

                var abastecimentoCriado = await _repository.AddAsync(entity);

                if (abastecimentoCriado is false)
                {
                    return new Response<AbastecimentoDespesaViagem?>(null, 500, "Internal Server Error");
                }

                var novaDespesa = new Despesa
                {
                    DataCompra = DateOnly.FromDateTime(DateTime.UtcNow.AddHours(-3)),
                    CentroCusto = "Abastecimento",
                    EntidadeId = entity.Id,
                    EntidadeOrigem = "Abastecimento",
                    ValorTotal = entity.ValorTotal,
                    Vencimento = DateOnly.FromDateTime(createRequest.DataVencimento),
                    Descricao = $"Abastecimento da viagem {entity.ViagemId} origem - ${entity.OrigemPagamento}",
                    FormaPagamento = "Cartão"
                };

                var despesaCriada = await _basedespesaRepository.AddAsync(novaDespesa);
                if (despesaCriada is false)
                {
                    await transaction.RollbackAsync();
                    return new Response<AbastecimentoDespesaViagem?>(null, 500, "Não foi possivel criar o abastecimento");
                }

                _cacheManager.ClearAll(typeof(Despesa).Name);
                await transaction.CommitAsync();

                return new Response<AbastecimentoDespesaViagem?>(new AbastecimentoDespesaViagem { Abastecimento = entity, Despesa = novaDespesa});
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<AbastecimentoDespesaViagem?>(null, 500, "Internal Server Error");
            }

        }

        public override async Task<Response<Abastecimento?>> RemoveAsync(int id)
        {
            using var transaction = await _repository.BeginTransactionAsync();
            try
            {
                var abastecimento = await _repository.GetByIdAsync(id);

                if (abastecimento is null)
                {
                    return new Response<Abastecimento?>(null, 404, "Abastecimento não econtrado");
                }

                var abastecimentoRemovido = await _repository.DeleteAsync(abastecimento);
                if (abastecimentoRemovido is false)
                {
                    return new Response<Abastecimento?>(null, 500, "Não foi possível remover o abastecimento");
                }

                var despesas = await _despesaRepository.GetByEntityId(id, "Abastecimento");
                if(despesas.Count > 0)
                {
                    var despesaRemovida = await _despesaRepository.DeleteAsync(abastecimento.Id, "Abastecimento");

                    if (despesaRemovida is false)
                    {
                        await transaction.RollbackAsync();
                        return new Response<Abastecimento?>(null, 500, "Não foi possivel remover, erro ao tentar remover despesa");
                    }
                }


                await transaction.CommitAsync();
                _cacheManager.ClearAll(typeof(Despesa).Name);
                return new Response<Abastecimento?>(null, message: "Removido com sucesso");

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<Abastecimento?>(null, 500, "Não foi possivel remover");
            }
        }
    }
}
