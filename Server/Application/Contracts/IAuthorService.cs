using Server.Domain.Results;
using Server.Application.Dtos;

namespace Server.Application.Contracts;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<AppResult<AuthorDto>> GetByIdAsync(int id);
    Task<AppResult<AuthorDto>> CreateAsync(CreateAuthorRequest request);
    Task<AppResult<AuthorDto>> UpdateAsync(int id, UpdateAuthorRequest request);
    Task<AppResult> DeleteAsync(int id);
}
