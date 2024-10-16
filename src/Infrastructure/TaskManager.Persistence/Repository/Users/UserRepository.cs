using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Domain.Identity;

namespace TaskManager.Persistence.Repository.Users;

public class UserRepository : IUserRepository
{
    private readonly IRepositoryAsync<User> _repository;

    public UserRepository(IRepositoryAsync<User> repository)
    {
        _repository = repository;
    }

    public async Task<User?> GetUserByIdWithRolesAsync(Guid id)
    {
        return await _repository.Entities.Include(x => x.Roles)
            .Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByUsernameWithRolesAsync(string username)
    {
        return await _repository.Entities
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
            .Where(x => x.Username == username)
            .AsNoTracking().FirstOrDefaultAsync();
    }
}
