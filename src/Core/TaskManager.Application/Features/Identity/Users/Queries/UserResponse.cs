namespace TaskManager.Application.Features.Identity.Users.Queries;

public class UserResponse
{
    public string Username { get; set; } = null!;
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool Enabled { get; set; }
    public bool IsDeleted { get; set; }
}
