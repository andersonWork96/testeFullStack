using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Domain.Entities;

public class Author
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Bio { get; set; }

    public DateTime? BirthDate { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
