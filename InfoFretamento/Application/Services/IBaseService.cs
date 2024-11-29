using InfoFretamento.Application.Responses;

namespace InfoFretamento.Application.Services
{
    public interface IBaseService<T, TCreateDto, TUpdateDto> where T : class
    {
        Task<Response<T?>> GetByIdAsync(int id);
        Task<Response<T?>> GetAllAsync();
        Task<Response<T?>> AddAsync(TCreateDto createRequest);
        Task<Response<T?>> UpdateAsync(TUpdateDto updateRequest);
        Task<Response<T?>> RemoveAsync(int id);

    }
}
