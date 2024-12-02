﻿using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services
{
    public class ClienteService(IBaseRepository<Cliente> repository) : BaseService<Cliente, AdicionarClienteRequest, AtualizarClienteRequest>(repository)
    {
       


    }
}
