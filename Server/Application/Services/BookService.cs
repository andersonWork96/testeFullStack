using Server.Application.Contracts;
using Server.Application.Dtos;
using Server.Domain.Entities;
using Server.Domain.Repositories;
using Server.Domain.Results;

namespace Server.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(
        IBookRepository bookRepository,
        IGenreRepository genreRepository,
        IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _genreRepository = genreRepository;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllWithDetailsAsync();
        return books.Select(MapToDto);
    }

    public async Task<AppResult<BookDto>> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdWithDetailsAsync(id);
        return book == null
            ? AppResult<BookDto>.NotFound()
            : AppResult<BookDto>.Success(MapToDto(book));
    }

    public async Task<AppResult<BookDto>> CreateAsync(CreateBookRequest request)
    {
        var genreExists = await _genreRepository.ExistsAsync(request.GenreId);
        var authorExists = await _authorRepository.ExistsAsync(request.AuthorId);

        if (!genreExists || !authorExists)
        {
            return AppResult<BookDto>.Invalid("Autor ou gênero inválido.");
        }

        var entity = new Book
        {
            Title = request.Title,
            Synopsis = request.Synopsis,
            PublishDate = request.PublishDate,
            GenreId = request.GenreId,
            AuthorId = request.AuthorId
        };

        await _bookRepository.AddAsync(entity);

        var created = await _bookRepository.GetByIdWithDetailsAsync(entity.Id) ?? entity;
        return AppResult<BookDto>.Success(MapToDto(created));
    }

    public async Task<AppResult<BookDto>> UpdateAsync(int id, UpdateBookRequest request)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return AppResult<BookDto>.NotFound();
        }

        var genreExists = await _genreRepository.ExistsAsync(request.GenreId);
        var authorExists = await _authorRepository.ExistsAsync(request.AuthorId);

        if (!genreExists || !authorExists)
        {
            return AppResult<BookDto>.Invalid("Autor ou gênero inválido.");
        }

        book.Title = request.Title;
        book.Synopsis = request.Synopsis;
        book.PublishDate = request.PublishDate;
        book.GenreId = request.GenreId;
        book.AuthorId = request.AuthorId;

        await _bookRepository.UpdateAsync(book);
        var updated = await _bookRepository.GetByIdWithDetailsAsync(id) ?? book;
        return AppResult<BookDto>.Success(MapToDto(updated));
    }

    public async Task<AppResult> DeleteAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return AppResult.NotFound();
        }

        await _bookRepository.DeleteAsync(book);
        return AppResult.Success();
    }

    private static BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Synopsis = book.Synopsis,
            PublishDate = book.PublishDate,
            GenreId = book.GenreId,
            AuthorId = book.AuthorId,
            GenreName = book.Genre?.Name,
            AuthorName = book.Author?.Name
        };
    }
}
