using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Application.Features.Identity.Users.Queries;
using TaskManager.Domain.Identity;

namespace TaskManager.Persistence.Repository;

public class UserRepository : IUserRepository
{
    private readonly IRepositoryAsync<User> _repository;

    public UserRepository(IRepositoryAsync<User> repository)
    {
        _repository = repository;
    }

    public async Task<List<UserResponse>> GetAllUserAsync()
    {
        return await _repository.Entities
            .Select(x => new UserResponse
            {
                Email = x.Email,
                Enabled = x.Enabled,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                IsDeleted = x.IsDeleted,
                Username = x.Username,
            })
            .AsNoTracking().ToListAsync();
    }

    public async Task<UserResponse?> GetUserByIdAsync(Guid id)
    {
        return await _repository.Entities
            .Where(x => x.Id == id)
            .Select(x => new UserResponse
            {
                Email = x.Email,
                Enabled = x.Enabled,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                IsDeleted = x.IsDeleted,
                Username = x.Username,
            })
            .AsNoTracking().FirstOrDefaultAsync();
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
