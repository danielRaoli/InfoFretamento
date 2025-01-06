    using InfoFretamento.Application.Request.ViagemRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ViagemService(IBaseRepository<Viagem> repository, IBaseRepository<Receita> receitaRepository, IBaseRepository<Veiculo> veiculoRepository, IMemoryCache memoryCache, CacheManager cacheManager) : BaseService<Viagem, AdicionarViagemRequest, AtualizarViagemRequest>(repository, memoryCache,cacheManager)
    {
        private readonly IBaseRepository<Viagem> _repository = repository;
        private readonly IBaseRepository<Receita> _receitaRepository = receitaRepository;
        private readonly IBaseRepository<Veiculo> _veiculoRepository = veiculoRepository;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly CacheManager _cacheManager = cacheManager;
        public override async Task<Response<Viagem?>> AddAsync(AdicionarViagemRequest createRequest)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var result = await base.AddAsync(createRequest);

                if (!result.IsSucces || result.Data == null)
                {
                    return result;
                }

                if (result.Data.Status.Equals("CONFIRMADO", StringComparison.OrdinalIgnoreCase))
                {
                    var receita = new Receita
                    {
                        CentroCusto = "viagens",
                        ResponsavelId = result.Data.ClienteId,
                        DataCompra = DateOnly.FromDateTime(DateTime.Now),
                        ViagemId = result.Data.Id,
                        ValorTotal = result.Data.ValorContratado,
                        OrigemPagamento = "cliente",
                    };

                    var receitaCriada = await _receitaRepository.AddAsync(receita);

                    if (!receitaCriada)
                    {
                        throw new Exception("Erro ao criar a receita associada.");
                    }

                    result.Message = "Viagem confirmada e receita criada com sucesso.";
                }

                await transaction.CommitAsync();
                _cacheManager.ClearAll($"{typeof(Viagem).Name}");
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<Viagem?>(null, 500, ex.Message);
            }
        }

        public async Task<Response<Viagem>> GetWithFilter(int id)
        {
            var response = await _repository.GetWithFilterAsync(id, new string[] { "Receita", "Despesas", "Motorista", "Cliente", "Veiculo", "Adiantamento", "Abastecimento" });
            return new Response<Viagem>(response);
        }

        public async Task<Response<List<Viagem>>> GetAllWithFilters(DateOnly startDate, DateOnly endDate, string? prefixoVeiculo = null)
        {
            var cacheKey = $"{typeof(Viagem).Name}_{startDate}_{endDate}_{prefixoVeiculo ?? "null"}";

            var viagens = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);

                var filters = new List<Expression<Func<Viagem, bool>>>
            {
                d => d.DataHorarioSaida.Data >= startDate,
                d => d.DataHorarioSaida.Data <= endDate
            };

                if (!string.IsNullOrEmpty(prefixoVeiculo))
                    filters.Add(d => d.Veiculo.Prefixo == prefixoVeiculo);

                var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Motorista", "Cliente", "Veiculo" });
                return response.ToList();
            });

            _cacheManager.AddKey(cacheKey);

            return new Response<List<Viagem>>(viagens);
        }

        public override async Task<Response<Viagem?>> UpdateAsync(AtualizarViagemRequest updateRequest)
        {
            // Busca a viagem pelo ID
            var viagem = await _repository.GetWithFilterAsync(updateRequest.Id, "Receita");
            if (viagem is null)
            {
                return new Response<Viagem?>(null, 404, "A viagem não existe.");
            }

            // Atualiza os dados da viagem
            viagem = updateRequest.UpdateEntity(viagem);

            // Inicia uma transação
            using var transaction = await _repository.BeginTransactionAsync();
            try
            {
                // Atualiza a viagem
                var viagemAtualizada = await _repository.UpdateAsync(viagem);
                if (!viagemAtualizada)
                {
                    await transaction.RollbackAsync();
                    return new Response<Viagem?>(null, 500, "Erro ao atualizar a viagem.");
                }

                // Verifica se o status da viagem é CONFIRMADO
                if (viagem.Status.Equals("CONFIRMADO", StringComparison.OrdinalIgnoreCase))
                {

                    if (viagem.Receita == null)
                    {
                        // Cria uma nova receita associada à viagem
                        viagem.Receita = new Receita
                        {
                            ViagemId = viagem.Id,
                            DataPagamento = DateOnly.FromDateTime(DateTime.Now),
                            DataCompra = DateOnly.FromDateTime(DateTime.Now),
                            OrigemPagamento = "cliente",
                            ResponsavelId = viagem.ClienteId,
                            ValorTotal = viagem.ValorContratado,
                            ValorParcial = 0,
                            FormaPagamento = viagem.TipoPagamento,
                            CentroCusto = "Viagem"
                        };

                        await _receitaRepository.AddAsync(viagem.Receita);
                    }
                }

                // Verifica se o status da viagem é FINALIZADA
                if (viagem.Status.Equals("FINALIZADO", StringComparison.OrdinalIgnoreCase))
                {
                    // Atualiza o KM do veículo, caso seja diferente de 0
                    if (viagem.KmFinalVeiculo != 0)
                    {
                        var veiculo = await _veiculoRepository.GetWithFilterAsync(viagem.VeiculoId);
                        if (veiculo != null)
                        {
                            veiculo.KmAtual = viagem.KmFinalVeiculo;
                            await _veiculoRepository.UpdateAsync(veiculo);
                        }
                    }
                }

                // Confirma a transação
                await transaction.CommitAsync();
                _cacheManager.ClearAll($"{typeof(Viagem).Name}");
                return new Response<Viagem?>(viagem);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<Viagem?>(null, 500, $"Erro ao atualizar: {ex.Message}");
            }
        }
    }
}
