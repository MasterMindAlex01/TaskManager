using TaskManager.Domain.Identity;

namespace TaskManager.Application.Common.Persistence.Users;

public interface IUserRepository
{
    Task<User?> GetUserByIdWithRolesAsync(Guid id);
    Task<User?> GetUserByUsernameWithRolesAsync(string username);
}
