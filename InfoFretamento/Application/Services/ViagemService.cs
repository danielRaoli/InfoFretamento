using InfoFretamento.Application.Request.ViagemRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ViagemService(IBaseRepository<Viagem> repository, IMemoryCache cache) : BaseService<Viagem, AdicionarViagemRequest, AtualizarViagemRequest>(repository, cache)
    {
        private readonly IBaseRepository<Viagem> _repository = repository;
        private readonly IMemoryCache _memorycache = cache;

        public async Task<Response<Viagem>> GetWithFilter(int id)
        {
            var response = await _repository.GetWithFilterAsync(id, new string[] { "Receitas", "Despesas", "Motorista", "Cliente", "Veiculo" });
            return new Response<Viagem>(response);
        }
        public async Task<Response<List<Viagem>>> GetAllWithFilters(DateOnly startDate, DateOnly endDate, string? prefixoVeiculo = null)
        {
            string cacheKey = $"{typeof(Manutencao).Name}_{startDate.ToString()}_{endDate.ToString()}_{prefixoVeiculo ?? "all"}";

            var result = await _memorycache.GetOrCreateAsync(cacheKey, async entry =>
            {
                var filters = new List<Expression<Func<Viagem, bool>>>();


                filters.Add(d => d.DataHorarioSaida.Data >= startDate); // Converte DateOnly para DateTime

                filters.Add(d => d.DataHorarioSaida.Data <= endDate); // Converte DateOnly para DateTime


                filters.Add(d => d.Veiculo.Prefixo == prefixoVeiculo);

                var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Motorista", "Cliente", "Veiculo" });
                return response.ToList();
            });
            
            return new Response<List<Viagem>>(result);
        }
    }
}
