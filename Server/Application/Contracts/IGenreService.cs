using Server.Domain.Results;
using Server.Application.Dtos;

namespace Server.Application.Contracts;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllAsync();
    Task<AppResult<GenreDto>> GetByIdAsync(int id);
    Task<AppResult<GenreDto>> CreateAsync(CreateGenreRequest request);
    Task<AppResult<GenreDto>> UpdateAsync(int id, UpdateGenreRequest request);
    Task<AppResult> DeleteAsync(int id);
}
