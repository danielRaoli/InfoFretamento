﻿using InfoFretamento.Application.Request.ServicoRequest;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace InfoFretamento.Application.Services
{
    public class ServicoService(IBaseRepository<Servico> repository, IMemoryCache cache) : BaseService<Servico, AdicionarServicoRequest, AtualizarServicoRequest>(repository, cache)
    {
    }
}
