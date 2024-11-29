using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services;

public class ColaboradorService(IBaseRepository<Colaborador> repository) : IBaseService<Colaborador, AdicionarColaboradorRequest, AtualizarColaboradorRequest>
{
    private readonly IBaseRepository<Colaborador> _repository = repository;

    public async Task<Response<Colaborador?>> AddAsync(AdicionarColaboradorRequest createRequest)
    {
        var entity = createRequest.ToEntity();
        var result = await _repository.AddAsync(entity);

        if (!result)
            return new Response<Colaborador?>(null, 500, "Erro ao tentar adicionar colaborador, consulte a manutenção.");

        return new Response<Colaborador?>(entity, 201, "Colaborador adicionado com sucesso.");
    }

    public async Task<Response<List<Colaborador>>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return new Response<List<Colaborador>>(result.ToList());
    }

    public async Task<Response<Colaborador?>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return new Response<Colaborador?>(null, 404, "Colaborador não encontrado.");

        return new Response<Colaborador?>(entity, 200);
    }

    public async Task<Response<Colaborador?>> RemoveAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return new Response<Colaborador?>(null, 404, "Colaborador não encontrado.");

        var result = await _repository.DeleteAsync(entity);
        return result ? new Response<Colaborador?>(null, 200, "Colaborador removido com sucesso.")
                      : new Response<Colaborador?>(null, 500, "Erro ao remover colaborador.");
    }

    public async Task<Response<Colaborador?>> UpdateAsync(AtualizarColaboradorRequest updateRequest)
    {
        var entity = await _repository.GetByIdAsync(updateRequest.Id);
        if (entity is null)
            return new Response<Colaborador?>(null, 404, "Colaborador não encontrado.");

        entity.Nome = updateRequest.Nome;
        entity.DataNascimento = updateRequest.DataNascimento;
        entity.Telefone = updateRequest.Telefone;
        entity.Documento = updateRequest.Documento;
        entity.Endereco = updateRequest.Endereco;
        entity.Cpf = updateRequest.Cpf;

        var result = await _repository.UpdateAsync(entity);
        return result ? new Response<Colaborador?>(null, 200, "Colaborador atualizado com sucesso.")
                      : new Response<Colaborador?>(null, 500, "Erro ao atualizar colaborador.");
    }
}
