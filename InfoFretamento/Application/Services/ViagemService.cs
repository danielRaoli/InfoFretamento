using InfoFretamento.Application.Request.ViagemRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ViagemService(IBaseRepository<Viagem> repository, IBaseRepository<Receita> receitaRepository, IBaseRepository<Veiculo> veiculoRepository, IMemoryCache memoryCache, CacheManager cacheManager, MotoristaViagemRepository motoristaViagemRepository, DespesaRepository despesaRepository, IBaseRepository<Pagamento> pagamentoRepository) : BaseService<Viagem, AdicionarViagemRequest, AtualizarViagemRequest>(repository, memoryCache, cacheManager)
    {
        private readonly IBaseRepository<Viagem> _repository = repository;
        private readonly IBaseRepository<Receita> _receitaRepository = receitaRepository;
        private readonly DespesaRepository _despesaRepository = despesaRepository;
        private readonly IBaseRepository<Veiculo> _veiculoRepository = veiculoRepository;
        private readonly IBaseRepository<Pagamento> _pagamentoRepository = pagamentoRepository;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly CacheManager _cacheManager = cacheManager;
        private readonly MotoristaViagemRepository _motoristaViagemRepository = motoristaViagemRepository;
        public override async Task<Response<Viagem?>> AddAsync(AdicionarViagemRequest createRequest)
        {
            // Inicia uma transação
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                // Adiciona a viagem base
                var result = await base.AddAsync(createRequest);

                if (!result.IsSucces || result.Data == null)
                {
                    return result;
                }

                var viagem = result.Data;

                // Adiciona os motoristas associados
                if (createRequest.MotoristasId != null && createRequest.MotoristasId.Any())
                {
                    var motoristasViagens = createRequest.MotoristasId
                        .Select(id => new MotoristaViagem { ViagemId = viagem.Id, MotoristaId = id })
                        .ToList();


                    await _motoristaViagemRepository.AddRangeAsync(motoristasViagens);


                }

                // Verifica se o status da viagem é CONFIRMADO
                if (viagem.Status.Equals("CONFIRMADO", StringComparison.OrdinalIgnoreCase))
                {
                    
                    var receita = new Receita
                    {
                        CentroCusto = "viagens",
                        DataCompra = DateOnly.FromDateTime(DateTime.Now),
                        ViagemId = viagem.Id,
                        ValorTotal = viagem.ValorContratado,
                        OrigemPagamento = "cliente",
                        Vencimento = viagem.DataHorarioSaida.Data
                    };

                    var receitaCriada = await _receitaRepository.AddAsync(receita);

                    if (!receitaCriada)
                    {
                        await transaction.RollbackAsync();
                        return new Response<Viagem?>(null, 500, "Não foi possivel criar a viagem e seus respectivos pagamentos");
                    }

                    if (createRequest.ValorParcial > 0)
                    {
                        var pagamento = new Pagamento { ReceitaId = receita.Id, DataPagamento = DateOnly.FromDateTime(DateTime.Now), ValorPago = createRequest.ValorParcial };
                        var pagamentoCriado = await _pagamentoRepository.AddAsync(pagamento);
                        if (!pagamentoCriado)
                        {
                            await transaction.RollbackAsync();
                            return new Response<Viagem?>(null, 500, "Não foi possivel criar a viagem e seus respectivos pagamentos");
                        }
                    }

                    result.Message = "Viagem confirmada, motoristas associados e receita criada com sucesso.";
                    _cacheManager.ClearAll($"{typeof(Receita).Name}");
                }
                else
                {
                    await _motoristaViagemRepository.SaveChangesAsync();
                }

                // Confirma a transação
                await transaction.CommitAsync();
                _cacheManager.ClearAll($"{typeof(Viagem).Name}");
                return result;
            }
            catch (Exception ex)
            {
                // Faz o rollback da transação em caso de erro
                await transaction.RollbackAsync();
                return new Response<Viagem?>(null, 500, ex.Message);
            }
        }

        public async Task<Response<ViagemResponse>> GetWithFilter(int id)
        {
            var viagem = await _repository.GetWithFilterAsync(id, new string[] {
        "Receita",
        "Receita.Pagamentos",
        "MotoristaViagens",
        "MotoristaViagens.Motorista",
        "Cliente",
        "Veiculo",
        "Adiantamento",
        "Abastecimentos"
    });

            if (viagem == null)
            {
                return new Response<ViagemResponse>(null, 404, "Viagem não encontrada");
            }

            var despesas = await _despesaRepository.GetByEntityId(id, "Viagem");


            return new Response<ViagemResponse>(new ViagemResponse
            {
                Viagem = viagem,
                Despesas = despesas,
            });
        }

        public async Task<Response<List<Viagem>>> GetAllWithFilters(DateOnly startDate, DateOnly endDate, string? prefixoVeiculo = null)
{
    var cacheKey = $"{typeof(Viagem).Name}_{startDate}_{endDate}_{prefixoVeiculo ?? "null"}";

    var viagens = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
        entry.SlidingExpiration = TimeSpan.FromMinutes(10);

        var filters = new List<Expression<Func<Viagem, bool>>>();

        filters.Add(v => (v.DataHorarioSaida.Data >= startDate && v.DataHorarioSaida.Data <= endDate) || (v.DataHorarioChegada.Data >= startDate && v.DataHorarioChegada.Data <= endDate) || ((v.DataHorarioRetorno.Data >= startDate && v.DataHorarioRetorno.Data <= endDate)) || v.Status == "PENDENTE");


        if (!string.IsNullOrEmpty(prefixoVeiculo))
            filters.Add(d => d.Veiculo.Prefixo == prefixoVeiculo);

        var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Receita", "Receita.Pagamentos", "Abastecimentos", "MotoristaViagens", "MotoristaViagens.Motorista", "Cliente", "Adiantamento", "Veiculo" });
        return response.ToList();
    });

    _cacheManager.AddKey(cacheKey);

    return new Response<List<Viagem>>(viagens);
}

public override async Task<Response<Viagem?>> UpdateAsync(AtualizarViagemRequest updateRequest)
{
    // Busca a viagem pelo ID
    var viagem = await _repository.GetWithFilterAsync(updateRequest.Id, "Receita", "MotoristaViagens");
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

        // Gerenciar os motoristas associados (MotoristaViagens)
        var motoristasIdsAtualizados = updateRequest.MotoristasId;

        // Obter IDs atuais de motoristas associados
        var motoristasIdsExistentes = viagem.MotoristaViagens.Select(mv => mv.MotoristaId).ToList();

        // Identificar motoristas para adicionar
        var motoristasParaAdicionar = motoristasIdsAtualizados.Except(motoristasIdsExistentes)
            .Select(id => new MotoristaViagem { ViagemId = viagem.Id, MotoristaId = id });

        // Identificar motoristas para remover
        var motoristasParaRemover = viagem.MotoristaViagens
            .Where(mv => !motoristasIdsAtualizados.Contains(mv.MotoristaId))
            .ToList();

        // Adicionar novos motoristas
        await _motoristaViagemRepository.AddRangeAsync(motoristasParaAdicionar);

        // Remover motoristas não mais associados
        _motoristaViagemRepository.RemoveRangeAsync(motoristasParaRemover);

        await _motoristaViagemRepository.SaveChangesAsync();

        // Verifica se o status da viagem é CONFIRMADO
        if (viagem.Status.Equals("CONFIRMADO", StringComparison.OrdinalIgnoreCase))
        {
            if (viagem.Receita == null)
            {
                // Cria uma nova receita associada à viagem
                viagem.Receita = new Receita
                {
                    ViagemId = viagem.Id,
                    DataCompra = DateOnly.FromDateTime(DateTime.Now),
                    OrigemPagamento = "cliente",
                    ValorTotal = viagem.ValorContratado,
                    FormaPagamento = viagem.TipoPagamento,
                    CentroCusto = "Viagem",
                    Vencimento = viagem.DataHorarioSaida.Data
                };

                var receitaCriada = await _receitaRepository.AddAsync(viagem.Receita);
                if (!receitaCriada)
                {
                    await transaction.RollbackAsync();
                    return new Response<Viagem?>(null, 500, "Não foi possivel criar a viagem e seus respectivos pagamentos");
                }

                if (updateRequest.ValorParcial > 0)
                {
                    var pagamento = new Pagamento { ReceitaId = viagem.Receita.Id, DataPagamento = DateOnly.FromDateTime(DateTime.Now), ValorPago = updateRequest.ValorParcial };
                    var pagamentoCriado = await _pagamentoRepository.AddAsync(pagamento);
                    if (!pagamentoCriado)
                    {
                        await transaction.RollbackAsync();
                        return new Response<Viagem?>(null, 500, "Não foi possivel criar a viagem e seus respectivos pagamentos");
                    }
                }


                _cacheManager.ClearAll($"{typeof(Receita).Name}");
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
                _cacheManager.ClearAll($"{typeof(Veiculo).Name}");
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
