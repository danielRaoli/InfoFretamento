using InfoFretamento.Application.Request.PagamentosRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ReceitaService(IBaseRepository<Receita> repository) : BaseService<Receita, AdicionarReceitaRequest, AtualizarReceitaRequest>(repository)
    {
        private readonly IBaseRepository<Receita> _repository = repository;

        public async Task<Response<List<Receita?>>> GetAllWithFilterAsync(DateOnly dateStart , DateOnly dateEnd )
        {


                var filters = new List<Expression<Func<Receita, bool>>>();

                filters.Add(d => d.DataCompra >= dateStart); // Converte DateOnly para DateTime


                filters.Add(d => d.DataCompra <= dateEnd); // Converte DateOnly para DateTime

                var response = await _repository.GetAllWithFilterAsync(filters, new string[] { "Viagem", "Responsavel" });


            return new Response<List<Receita?>>(response.ToList());
        }
    }
}
