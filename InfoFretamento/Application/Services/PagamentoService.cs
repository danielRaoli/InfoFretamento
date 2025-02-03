using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class PagamentoService(IBaseRepository<Pagamento> repository, IMemoryCache cache, CacheManager cacheManager, IBaseRepository<Receita> receitaRepository) : BaseService<Pagamento, PagamentoRequest, AtualizarPagamentoRequest>(repository, cache, cacheManager)
    {
        private readonly IBaseRepository<Pagamento> _repository = repository;
        private readonly IBaseRepository<Receita> _receitaRepository = receitaRepository;

        public override async Task<Response<Pagamento?>> AddAsync(PagamentoRequest createRequest)
        {
            var receita = await _receitaRepository.GetWithFilterAsync(createRequest.ReceitaId, "Pagamentos");
            if (receita == null)
            {
                return new Response<Pagamento?>(null, 404, "Receita nao encontrada");
            }

            if(receita.ValorPago + createRequest.ValorPago > receita.ValorTotal || receita.Pago)
            {
                return new Response<Pagamento?>(null, 404, "O valor de pagamento é maior que o valor total da receita ou a receita já foi paga.");
            }

            return await base.AddAsync(createRequest);

        }
    }
}
