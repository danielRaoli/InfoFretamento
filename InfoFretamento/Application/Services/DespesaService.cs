using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class DespesaService(IBaseRepository<Despesa> repository, IMemoryCache cache) : BaseService<Despesa, AdicionarDespesaRequest, AtualizarDespesaRequest>(repository)
    {
        private readonly IBaseRepository<Despesa> _repository = repository;


        public async Task<Response<List<Despesa>>> GetAllWithFilterAsync(DateOnly dateStart, DateOnly dateEnd, string? veiculoPrefixo = null)
        {



            // Aplica os filtros
            var filters = new List<Expression<Func<Despesa, bool>>>();

            filters.Add(d => d.DataCompra >= dateStart);


            filters.Add(d => d.DataCompra <= dateEnd);



            if (!string.IsNullOrEmpty(veiculoPrefixo)) filters.Add(d => d.Viagem.Veiculo.Prefixo.Equals(veiculoPrefixo));


            // Busca os dados no repositório
            var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Viagem", "Viagem.Veiculo", "Responsavel" });


            return new Response<List<Despesa>>(response.ToList());
        }
    }
}
