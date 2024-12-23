using InfoFretamento.Application.Request.VeiculoRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class VeiculoService(IBaseRepository<Veiculo> repository, IMemoryCache cache) : BaseService<Veiculo, AdicionarVeiculoRequest, AtualizarVeiculoRequest>(repository, cache)
    {
       private readonly IBaseRepository<Veiculo> _repository = repository;
        
        public async Task<Response<Veiculo?>> GetWithInclude(int id)
        {
            var response = await _repository.GetWithFilterAsync(id, new string[] {"Viagens", "Manutencoes"});

            return new Response<Veiculo?>(response);
        }
    }
}
