using Server.Application.Contracts;
using Server.Application.Dtos;
using Server.Domain.Entities;
using Server.Domain.Repositories;
using Server.Domain.Results;

namespace Server.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync()
    {
        var authors = await _repository.GetAllAsync();
        return authors.Select(MapToDto);
    }

    public async Task<AppResult<AuthorDto>> GetByIdAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id);
        return author == null
            ? AppResult<AuthorDto>.NotFound()
            : AppResult<AuthorDto>.Success(MapToDto(author));
    }

    public async Task<AppResult<AuthorDto>> CreateAsync(CreateAuthorRequest request)
    {
        var author = new Author
        {
            Name = request.Name,
            Bio = request.Bio,
            BirthDate = request.BirthDate
        };

        var created = await _repository.AddAsync(author);
        return AppResult<AuthorDto>.Success(MapToDto(created));
    }

    public async Task<AppResult<AuthorDto>> UpdateAsync(int id, UpdateAuthorRequest request)
    {
        var author = await _repository.GetByIdAsync(id);
        if (author == null)
        {
            return AppResult<AuthorDto>.NotFound();
        }

        author.Name = request.Name;
        author.Bio = request.Bio;
        author.BirthDate = request.BirthDate;

        await _repository.UpdateAsync(author);
        return AppResult<AuthorDto>.Success(MapToDto(author));
    }

    public async Task<AppResult> DeleteAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id);
        if (author == null)
        {
            return AppResult.NotFound();
        }

        await _repository.DeleteAsync(author);
        return AppResult.Success();
    }

    private static AuthorDto MapToDto(Author author)
    {
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            Bio = author.Bio,
            BirthDate = author.BirthDate
        };
    }
}
