using Server.Application.Contracts;
using Server.Application.Dtos;
using Server.Domain.Entities;
using Server.Domain.Repositories;
using Server.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace Server.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;

    public GenreService(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        var genres = await _repository.GetAllAsync();
        return genres.Select(MapToDto);
    }

    public async Task<AppResult<GenreDto>> GetByIdAsync(int id)
    {
        var genre = await _repository.GetByIdAsync(id);
        return genre == null
            ? AppResult<GenreDto>.NotFound()
            : AppResult<GenreDto>.Success(MapToDto(genre));
    }

    public async Task<AppResult<GenreDto>> CreateAsync(CreateGenreRequest request)
    {
        var genre = new Genre
        {
            Name = request.Name,
            Description = request.Description
        };

        var created = await _repository.AddAsync(genre);
        return AppResult<GenreDto>.Success(MapToDto(created));
    }

    public async Task<AppResult<GenreDto>> UpdateAsync(int id, UpdateGenreRequest request)
    {
        var genre = await _repository.GetByIdAsync(id);
        if (genre == null)
        {
            return AppResult<GenreDto>.NotFound();
        }

        genre.Name = request.Name;
        genre.Description = request.Description;

        await _repository.UpdateAsync(genre);
        return AppResult<GenreDto>.Success(MapToDto(genre));
    }

    public async Task<AppResult> DeleteAsync(int id)
    {
        var genre = await _repository.GetByIdAsync(id);
        if (genre == null)
        {
            return AppResult.NotFound();
        }

        var hasBooks = await _repository.HasBooksAsync(id);
        if (hasBooks)
        {
            return AppResult.Invalid("Não é possível excluir o gênero porque existem livros associados.");
        }

        try
        {
            await _repository.DeleteAsync(genre);
            return AppResult.Success();
        }
        catch (DbUpdateException)
        {
            return AppResult.Invalid("Não é possível excluir o gênero porque existem livros associados.");
        }
    }

    private static GenreDto MapToDto(Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        };
    }
}
