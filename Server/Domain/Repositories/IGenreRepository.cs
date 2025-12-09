using Server.Domain.Entities;

namespace Server.Domain.Repositories;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllAsync();
    Task<Genre?> GetByIdAsync(int id);
    Task<Genre> AddAsync(Genre genre);
    Task UpdateAsync(Genre genre);
    Task DeleteAsync(Genre genre);
    Task<bool> ExistsAsync(int id);
    Task<bool> HasBooksAsync(int id);
}
