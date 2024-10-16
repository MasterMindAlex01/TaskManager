
namespace TaskManager.Application.Features.Identity.Roles.Queries;

public class RoleResponse
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Disabled { get; set; }
}
