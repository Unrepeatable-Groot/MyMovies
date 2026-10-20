namespace MyMovies.Application.Dtos;

public class ChangeEmailDto
{
    public string CurrentPassword { get; set; } = null!;
    public string NewEmail { get; set; } = null!;
}
