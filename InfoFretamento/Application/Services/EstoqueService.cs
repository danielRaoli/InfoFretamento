using InfoFretamento.Application.Request.PecasRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class EstoqueService(EstoqueRepository repository, PecaService service, VeiculoService veiculoService, CacheManager cacheManager, IMemoryCache memoryCache)
    {
        private readonly EstoqueRepository _repository = repository;
        private readonly PecaService _pecaService = service;
        private readonly VeiculoService _veiculoService = veiculoService;
        private readonly CacheManager _cacheManager = cacheManager;
        private readonly IMemoryCache _memoryCache = memoryCache;


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
            var peca = await _pecaService.GetByIdAsync(request.PecaId);

            if (peca.Data == null)
            {
                return new Response<RetiradaPeca>(null, 404, "Peça não encontrada");
            }

            var veiculo = await _veiculoService.GetByIdAsync(request.VeiculoId);

            if (veiculo.Data == null)
            {
                return new Response<RetiradaPeca>(null, 404, "Peça não encontrada");
            }

            if (request.Quantidade > peca.Data.Quantidade || request.Quantidade < 0 || request.Quantidade == 0)
            {
                return new Response<RetiradaPeca>(null, 400, "Quantidade de peças inseridas não está disponivel no estoque ou o valor é inválido");
            }

            request.PrecoTotal = request.Quantidade * peca.Data.Preco;

            peca.Data.Quantidade = peca.Data.Quantidade - request.Quantidade;

            var estoqueAtualizado = await _repository.AtualizarEstoqueAsync(peca.Data);
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
            var peca = await _pecaService.GetByIdAsync(request.PecaId);

            if (peca.Data == null)
            {
                return new Response<AdicionarPeca?>(null, 404, "Peça não encontrada");
            }


            if (request.Quantidade < 0 || request.Quantidade == 0)
            {
                return new Response<AdicionarPeca?>(null, 400, "Valor da quantidade de peças é inválido");
            }

            request.PrecoTotal = request.Quantidade * peca.Data.Preco;

            peca.Data.Quantidade = peca.Data.Quantidade + request.Quantidade;

            var estoqueAtualizado = await _repository.AtualizarEstoqueAsync(peca.Data);
            if (!estoqueAtualizado)
            {
                return new Response<AdicionarPeca?>(null, 500, "Nao foi possivel atualizar o estoque no momento");
            }

            var entity = request.ToEntity();

            var result = await _repository.AddPecaEstoque(entity);

            if (result is null)
            {
                new Response<AdicionarPeca?>(null, 500, "Nao foi possivel atualizar o estoque no momento");
            }

            _cacheManager.ClearAll($"{typeof(RetiradaPeca).Name}");
            _cacheManager.ClearAll($"{typeof(Peca).Name}");
            return new Response<AdicionarPeca?>(result);

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



        public async Task<Response<RetiradaPeca>> RemoverAdicionamentos(int id)
        {
            var adicionamento = await _repository.GetAdicionamentoById(id);
            if (adicionamento == null)
            {
                return new Response<RetiradaPeca>(null, 404, "Retirada de peca nao encontrada no historico");
            }

            var result = await _repository.RemoveAdicionamento(adicionamento);
            if (!result)
            {
                new Response<RetiradaPeca>(null, 500, "Erro ao tentar remover retirada do historico");
            }

            _cacheManager.ClearAll($"{typeof(RetiradaPeca).Name}");
            _cacheManager.ClearAll($"{typeof(Peca).Name}");
            return new Response<RetiradaPeca>(null);
        }

    }

}
