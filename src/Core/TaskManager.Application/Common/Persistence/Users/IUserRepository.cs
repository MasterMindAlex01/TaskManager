using TaskManager.Domain.Identity;

namespace TaskManager.Application.Common.Persistence.Users;

public interface IUserRepository
{
    Task<User?> GetByIdWithRolesAsync(Guid id);
}
