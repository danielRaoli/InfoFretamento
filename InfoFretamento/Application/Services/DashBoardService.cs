using InfoFretamento.Application.Responses;
using InfoFretamento.Domain.ValueObjects;
using InfoFretamento.Infrastructure.Repositories;

namespace InfoFretamento.Application.Services
{
    public class DashBoardService(DashBoardRepository repository)
    {
        private readonly DashBoardRepository _repository = repository;  

        public async Task<Response<int>> TotalViagens()
        {
            var result = await _repository.TotalViagens();
            return new Response<int>(result);
        }

        public async Task<Response<decimal>> TotalDespesas()
        {
            var result = await _repository.DespesasMensais();
            return new Response<decimal>(result);
        }


        public async Task<Response<decimal>> TotalReceitas()
        {
            var result = await _repository.ReceitasMensais();
            return new Response<decimal>(result);
        }

        public async Task<Response<List<ReceitasMensais>>> ValorLiquidoMensal(int ano)
        {
            var result = await _repository.ValorLiquidoMensal(ano);
            return new Response<List<ReceitasMensais>>(result);
        }
    }
}
