using Microsoft.EntityFrameworkCore;
using Server.Domain.Entities;
using Server.Domain.Repositories;
using Server.Infrastructure.Persistence;

namespace Server.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly Context _context;

    public GenreRepository(Context context)
    {
        _context = context;
    }

    public async Task<Genre> AddAsync(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return genre;
    }

    public async Task DeleteAsync(Genre genre)
    {
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Genres.AnyAsync(g => g.Id == id);
    }

    public async Task<List<Genre>> GetAllAsync()
    {
        return await _context.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        return await _context.Genres.FindAsync(id);
    }

    public async Task<bool> HasBooksAsync(int id)
    {
        return await _context.Books.AnyAsync(b => b.GenreId == id);
    }

    public async Task UpdateAsync(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }
}
