using InfoFretamento.Application.Request;
using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.Entities;
using InfoFretamento.Domain.Repositories;

namespace InfoFretamento.Application.Services;

public class FornecedorService(IBaseRepository<Fornecedor> repository) : IBaseService<Fornecedor, AdicionarFornecedorRequest, AtualizarFornecedorRequest>
{
    private readonly IBaseRepository<Fornecedor> _repository = repository;

    public async Task<Response<Fornecedor?>> AddAsync(AdicionarFornecedorRequest createRequest)
    {
        var entity = createRequest.ToEntity();
        var result = await _repository.AddAsync(entity);

        if (!result)
            return new Response<Fornecedor?>(null, 500, "Erro ao tentar adicionar fornecedor, consulte a manutenção.");

        return new Response<Fornecedor?>(entity, 201, "Fornecedor adicionado com sucesso.");
    }

    public async Task<Response<List<Fornecedor>>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        return new Response<List<Fornecedor>>(result.ToList());
    }

    public async Task<Response<Fornecedor?>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return new Response<Fornecedor?>(null, 404, "Fornecedor não encontrado.");

        return new Response<Fornecedor?>(entity, 200);
    }

    public async Task<Response<Fornecedor?>> RemoveAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return new Response<Fornecedor?>(null, 404, "Fornecedor não encontrado.");

        var result = await _repository.DeleteAsync(entity);
        return result ? new Response<Fornecedor?>(null, 200, "Fornecedor removido com sucesso.")
                      : new Response<Fornecedor?>(null, 500, "Erro ao remover fornecedor.");
    }

    public async Task<Response<Fornecedor?>> UpdateAsync(AtualizarFornecedorRequest updateRequest)
    {
        var entity = await _repository.GetByIdAsync(updateRequest.Id);
        if (entity is null)
            return new Response<Fornecedor?>(null, 404, "Fornecedor não encontrado.");

        entity.Nome = updateRequest.Nome;
        entity.DataNascimento = updateRequest.DataNascimento;
        entity.Telefone = updateRequest.Telefone;
        entity.Documento = updateRequest.Documento;
        entity.Endereco = updateRequest.Endereco;
        entity.Cpf = updateRequest.Cpf;
        entity.TipoPessoa = updateRequest.TipoPessoa;

        var result = await _repository.UpdateAsync(entity);
        return result ? new Response<Fornecedor?>(null, 200, "Fornecedor atualizado com sucesso.")
                      : new Response<Fornecedor?>(null, 500, "Erro ao atualizar fornecedor.");
    }
}
