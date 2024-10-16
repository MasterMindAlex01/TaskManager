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

    public async Task<User?> GetByIdWithRolesAsync(Guid id)
    {
        return await _repository.Entities.Include(x => x.Roles)
            .Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
