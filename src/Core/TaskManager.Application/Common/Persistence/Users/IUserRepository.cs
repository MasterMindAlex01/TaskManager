using TaskManager.Application.Features.Identity.Users.Queries;
using TaskManager.Domain.Identity;

namespace TaskManager.Application.Common.Persistence.Users;

public interface IUserRepository
{
    Task<User?> GetUserByIdWithRolesAsync(Guid id);
    Task<User?> GetUserByUsernameWithRolesAsync(string username);
    Task<UserResponse?> GetUserByIdAsync(Guid id);
    Task<List<UserResponse>> GetAllUserAsync();
}
