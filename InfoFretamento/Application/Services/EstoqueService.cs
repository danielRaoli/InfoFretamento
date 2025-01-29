using InfoFretamento.Application.Request.PecasRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class EstoqueService(EstoqueRepository repository, PecaService service, IBaseRepository<Veiculo> veiculoService, IBaseRepository<Peca> pecaRepository, CacheManager cacheManager, IMemoryCache memoryCache, IBaseRepository<Despesa> baseDespesa, DespesaRepository despesaRepository, BoletoRepository boletoRepository)
    {
        private readonly EstoqueRepository _repository = repository;
        private readonly IBaseRepository<Peca> _pecaRepository = pecaRepository;
        private readonly IBaseRepository<Veiculo> _veiculoService = veiculoService;
        private readonly CacheManager _cacheManager = cacheManager;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly BoletoRepository _boletoRepository = boletoRepository;
        private readonly IBaseRepository<Despesa> _baseDespesa = baseDespesa;
        private readonly DespesaRepository _despesaRepository = despesaRepository;


        public async Task<Response<List<RetiradaPeca>>> GetAllRetiradas(DateOnly minDate, DateOnly maxDate, string? prefixoVeiculo = null)

        {
            var cacheKey = $"{typeof(RetiradaPeca).Name}_{minDate}_{maxDate}_{prefixoVeiculo ?? "null"}";
            var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                _cacheManager.AddKey(cacheKey);
                return await _repository.GetAllRetiradas(minDate, maxDate, prefixoVeiculo);
            });


            return new Response<List<RetiradaPeca>>(result);
        }

        public async Task<Response<List<AdicionarPeca>>> GetAllAdicionamentos(DateOnly minDate, DateOnly maxDate)
        {
            var cacheKey = $"{typeof(AdicionarPeca).Name}_{minDate}_{maxDate}";
            var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                _cacheManager.AddKey(cacheKey);
                return await _repository.GetAllAdicionamentos(minDate, maxDate);
            });


            return new Response<List<AdicionarPeca>>(result);
        }

        public async Task<Response<RetiradaPeca>> RetirarPeca(RetirarPecaRequest request)
        {
            var peca = await _pecaRepository.GetByIdAsync(request.PecaId);

            if (peca == null)
            {
                return new Response<RetiradaPeca>(null, 404, "Peça não encontrada");
            }

            var veiculo = await _veiculoService.GetByIdAsync(request.VeiculoId);

            if (veiculo == null)
            {
                return new Response<RetiradaPeca>(null, 404, "Peça não encontrada");
            }

            if (request.Quantidade > peca.Quantidade || request.Quantidade < 0 || request.Quantidade == 0)
            {
                return new Response<RetiradaPeca>(null, 400, "Quantidade de peças inseridas não está disponivel no estoque ou o valor é inválido");
            }

            request.PrecoTotal = request.Quantidade * peca.Preco;

            peca.Quantidade = peca.Quantidade - request.Quantidade;

            var estoqueAtualizado = await _repository.AtualizarEstoqueAsync(peca);
            if (!estoqueAtualizado)
            {
                return new Response<RetiradaPeca>(null, 500, "Nao foi possivel atualizar o estoque no momento");
            }

            var entity = request.ToEntity();

            var result = await _repository.AddRetiradaAsync(entity);

            if (result is null) { new Response<RetiradaPeca>(null, 500, "Nao foi possivel atualizar o estoque no momento"); }

            _cacheManager.ClearAll($"{typeof(RetiradaPeca).Name}");
            _cacheManager.ClearAll($"{typeof(Peca).Name}");

            return new Response<RetiradaPeca>(result);

        }



        public async Task<Response<AdicionarPeca?>> AdicionarPeca(AdicionarPecaRequest request)
        {
            using var transaction = await _pecaRepository.BeginTransactionAsync();
            try
            {
                var peca = await _pecaRepository.GetByIdAsync(request.PecaId);

                if (peca == null)
                {
                    return new Response<AdicionarPeca?>(null, 404, "Peça não encontrada");
                }


                if (request.Quantidade < 0 || request.Quantidade == 0)
                {
                    return new Response<AdicionarPeca?>(null, 400, "Valor da quantidade de peças é inválido");
                }

                request.PrecoTotal = request.Quantidade * peca.Preco;


                var entity = request.ToEntity();

                var result = await _repository.AddPecaEstoque(entity);
                if (result is null)
                {
                    await transaction.RollbackAsync();
                    return new Response<AdicionarPeca?>(null, 500, "Nao foi possivel atualizar o estoque no momento");
                }

                peca.Quantidade = peca.Quantidade + request.Quantidade;

                var estoqueAtualizado = await _repository.AtualizarEstoqueAsync(peca);
                if (!estoqueAtualizado)
                {
                    await transaction.RollbackAsync();
                    return new Response<AdicionarPeca?>(null, 500, "Nao foi possivel atualizar o estoque no momento");
                }

                var despesa = new Despesa
                {
                    DataCompra = entity.DataDeEntrada,
                    Vencimento = DateOnly.FromDateTime(request.TipoPagamento == "Boleto" ? request.Vencimentos.FirstOrDefault(): request.Vencimento.Value),
                    EntidadeOrigem = "Estoque",
                    EntidadeId = entity.Id,
                    ValorTotal = entity.PrecoTotal,
                    FormaPagamento = request.TipoPagamento,
                    Parcelas = request.TipoPagamento.ToUpper().Equals("BOLETO") ? request.Parcelas : 1,
                    CentroCusto = "Estoque",
                    Descricao = $"Despesa gerada para o estoque da peca {entity.Peca.Nome}",
                };
              
                var despesaCriada = await _baseDespesa.AddAsync(despesa);
                if (!despesaCriada)
                {
                    await transaction.RollbackAsync();
                    return new Response<AdicionarPeca?>(null, 500, "erro ao tentar atualizar estoque");
                }

                if (request.TipoPagamento.ToUpper() == "BOLETO")
                {
                   
                    if (request.Vencimentos.Count != request.Parcelas)
                    {
                        throw new ArgumentException("O número de vencimentos fornecido não corresponde ao número de parcelas.");
                    }

                    var boletos = request.Vencimentos.Select((vencimento, index) => new Boleto
                    {
                        Referencia = $"Reestoque-{entity.Id}-Parcela-{index + 1}",
                        DataEmissao = entity.DataDeEntrada,
                        Valor = entity.PrecoTotal / request.Parcelas,
                        Juros = 0, // Assumindo sem juros
                        DespesaId = despesa.Id,
                        Vencimento = DateOnly.FromDateTime(vencimento),
                        Pago = false
                    }).ToList();

                    // Adicionar boletos ao banco de dados
                    await _boletoRepository.AddRange(boletos);

                    despesa.Boletos = boletos;
                }


                await transaction.CommitAsync();


                _cacheManager.ClearAll($"{typeof(AdicionarPeca).Name}");
                _cacheManager.ClearAll($"{typeof(Peca).Name}");
                _cacheManager.ClearAll($"{typeof(Despesa).Name}");
                return new Response<AdicionarPeca?>(entity);

            }
            catch (Exception ex) {
                await transaction.RollbackAsync();

                return new Response<AdicionarPeca?>(null, 500, "erro ao tentar registrar reestoque");
            
            }


        }



        public async Task<Response<RetiradaPeca>> RemoverRetirada(int id)
        {
            var retirada = await _repository.GetRetiradaById(id);
            if (retirada == null)
            {
                return new Response<RetiradaPeca>(null, 404, "Retirada de peca nao encontrada no historico");
            }

            var result = await _repository.RemoveRetirada(retirada);
            if (!result)
            {
                new Response<RetiradaPeca>(null, 500, "Erro ao tentar remover retirada do historico");
            }
            _cacheManager.ClearAll($"{typeof(RetiradaPeca).Name}");
            _cacheManager.ClearAll($"{typeof(Peca).Name}");
            return new Response<RetiradaPeca>(null);
        }



        public async Task<Response<AdicionarPeca>> RemoverAdicionamentos(int id)
        {
            using var transaction = await _pecaRepository.BeginTransactionAsync();
            try
            {
                var adicionamento = await _repository.GetAdicionamentoById(id);

                if (adicionamento == null)
                {
                    return new Response<AdicionarPeca>(null, 404, "Registro de reestoque de peca nao encontrada no historico");
                }


                var result = await _repository.RemoveAdicionamento(adicionamento);
                if (!result)
                {
                    new Response<RetiradaPeca>(null, 500, "Erro ao tentar remover registro reestoque do historico");
                }




                 result = await _despesaRepository.DeleteAsync(id, "Estoque");
                if (!result) {
                    await transaction.RollbackAsync();
                }

                await transaction.CommitAsync();    

                _cacheManager.ClearAll($"{typeof(AdicionarPeca).Name}");
                _cacheManager.ClearAll($"{typeof(Peca).Name}");
                _cacheManager.ClearAll($"{typeof(Despesa).Name}");
                return new Response<AdicionarPeca>(null);
            }
            catch (Exception ex) { 
                await transaction.RollbackAsync();
                return new Response<AdicionarPeca>(null, 500, "erro ao tentar remover reestoque do historico");
            }

        }


        public async Task<Response<ReestoqueResponse>> GetById(int id)
        {
            var reestoque = await _repository.GetAdicionamentoById(id);
            if(reestoque == null)
            {
                return new Response<ReestoqueResponse>(null, 404, "Reestoque nao encontrado no estoque");
            }
            

            var despesa = await _despesaRepository.GetByEntityId(reestoque.Id, "Estoque");

            return new Response<ReestoqueResponse>(new ReestoqueResponse { AdicionarPeca = reestoque, Despesa = despesa.FirstOrDefault() });

        }

    }

}
