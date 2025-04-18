﻿using InfoFretamento.Application.Request.MotoristaRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class MotoristaService(IBaseRepository<Motorista> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BasePessoaService<Motorista, AdicionarMotoristaRequest, AtualizarMotoristaRequest>(repository, memoryCache, cacheManager)
    {
        private readonly IBaseRepository<Motorista> _repository = repository;

        public async Task<Response<Motorista?>> GetWithIncludes(int id)
        {
            var response = await _repository.GetWithFilterAsync(id, new string[] { "Ferias" });
            return response != null ? new Response<Motorista?>(response) : new Response<Motorista?>(null, 404, "motorista não encontrado");
        }
    }
}
