using System.Linq.Expressions;
using System;
using InfoFretamento.Application.Request.ViagemProgramadaRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class ViagemProgramadaService(IBaseRepository<ViagemProgramada> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BaseService<ViagemProgramada, AdicionarViagemProgramadaRequest, AtualizarViagemProgramadaRequest>(repository, memoryCache, cacheManager)
    {
        private readonly IBaseRepository<ViagemProgramada> _repository = repository;
        public async Task<Response<ViagemProgramada?>> GetByIdIncludeAsync(int id)
        {

            // Chama o repositório genérico
            var viagem = await _repository.GetWithFilterAsync(
                        id: id,
                        includes: new string[]
                        {
                            "Passagens",
                            "Veiculo"
                        }
                    );

            return viagem is not null
                ? new Response<ViagemProgramada?>(viagem)
                : new Response<ViagemProgramada?>(null, 404, "Nenhuma viagem encontrada");
        }


        public  async Task<Response<List<ViagemProgramada>>> GetAllWithIncludes (int ano, int mes)
        {
            var firstDay = new DateOnly(ano, mes, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            var filters = new List<Expression<Func< ViagemProgramada, bool>>>()
            {
                x => x.Saida.Data >= firstDay && x.Saida.Data <= lastDay
            };

            // Chama o repositório genérico
            var viagem = await _repository.GetAllWithFilterAsync(
                      filters: filters,
                        includes: new string[]
                        {
                            "Passagens",
                            "Veiculo"
                        }
                    );

            return new Response<List<ViagemProgramada>>(viagem.ToList());
              
        }
    }
}
