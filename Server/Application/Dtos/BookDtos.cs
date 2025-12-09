namespace Server.Application.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Synopsis { get; set; }
    public DateTime? PublishDate { get; set; }
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
    public string? GenreName { get; set; }
    public string? AuthorName { get; set; }
}

public class CreateBookRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Synopsis { get; set; }
    public DateTime? PublishDate { get; set; }
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
}

public class UpdateBookRequest : CreateBookRequest
{
}
