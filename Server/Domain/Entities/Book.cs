using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Domain.Entities;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Title { get; set; }

    [MaxLength(1000)]
    public string? Synopsis { get; set; }

    public DateTime? PublishDate { get; set; }

    [ForeignKey("Genre")]
    public int GenreId { get; set; }

    [ForeignKey("Author")]
    public int AuthorId { get; set; }

    public Genre Genre { get; set; } = null!;
    public Author Author { get; set; } = null!;
}
