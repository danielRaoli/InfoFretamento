using InfoFretamento.Application.Request.ManutencaoRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace InfoFretamento.Application.Services
{
    public class ManutencaoService(IBaseRepository<Manutencao> repository) : BaseService<Manutencao, AdicionarManutencaoRequest, AtualizarManutencaoRequest>(repository)
    {
        private readonly IBaseRepository<Manutencao> _repository = repository;


        public async Task<Response<List<Manutencao>>> GetAllWithFilters(DateOnly startDate, string? situacao = null )
        {


                var filters = new List<Expression<Func<Manutencao, bool>>>();

                filters.Add(m => m.DataLancamento >= startDate);
                var response = await _repository.GetAllWithFilterAsync(filters: filters, includes: new string[] { "Veiculo", "Servico" });

                if (response != null && situacao != null)
                {
                    response = response.Where(m => m.Situacao.Equals(situacao));
                }



            return new Response<List<Manutencao>>(response.ToList());

        }
    }
}
