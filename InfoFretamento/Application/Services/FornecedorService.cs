﻿using InfoFretamento.Application.Request.FornecedorRequest;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services;

public class FornecedorService(IBaseRepository<Fornecedor> repository, IMemoryCache memoryCache, CacheManager cacheManager) : BasePessoaService<Fornecedor, AdicionarFornecedorRequest, AtualizarFornecedorRequest>(repository, memoryCache, cacheManager)
{
    private readonly IBaseRepository<Fornecedor> _repository = repository;

    public async Task<Response<Fornecedor?>> GetWithIncludes(int id)
    {
        var response = await _repository.GetWithFilterAsync(id, new string[] { "Despesas", "Receitas" });
        return response != null ? new Response<Fornecedor?>(response) : new Response<Fornecedor?>(null, 404, "motorista não encontrado");
    }
}
