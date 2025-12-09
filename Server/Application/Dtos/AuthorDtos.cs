namespace Server.Application.Dtos;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }
}

public class CreateAuthorRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }
}

public class UpdateAuthorRequest : CreateAuthorRequest
{
}
