using Server.Domain.Results;
using Server.Application.Dtos;

namespace Server.Application.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<AppResult<BookDto>> GetByIdAsync(int id);
    Task<AppResult<BookDto>> CreateAsync(CreateBookRequest request);
    Task<AppResult<BookDto>> UpdateAsync(int id, UpdateBookRequest request);
    Task<AppResult> DeleteAsync(int id);
}
