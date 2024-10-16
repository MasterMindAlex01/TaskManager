using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Application.Features.Identity.Roles.Queries;
using TaskManager.Domain.Identity;

namespace TaskManager.Persistence.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly IRepositoryAsync<Role> _repository;

    public RoleRepository(IRepositoryAsync<Role> repository)
    {
        _repository = repository;
    }

    public async Task<RoleResponse?> GetRoleByIdAsync(Guid roleId)
    {
        return await _repository.Entities
            .Where(x => x.Id == roleId)
            .Select(x => new RoleResponse
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description,
                Disabled = x.IsDisabled,
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<List<RoleResponse>> GetAllAsync()
    {
        return await _repository.Entities
            .Select(x => new RoleResponse
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description,
                Disabled = x.IsDisabled,
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Role?> GetRoleByIdWithUsersAsync(Guid roleId)
    {
        return await _repository.Entities
            .Include(x => x.Users)
            .Where(x => x.Id == roleId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<List<Role>> GetRoleListByIdListAsync(List<Guid> roleIdList)
    {
        return await _repository.Entities
            .Where(x => roleIdList.Contains(x.Id))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Role?> GetRoleByNameAsync(string rolename)
    {
        return await _repository.Entities
            .Include(x => x.Users)
            .Where(x => x.Name == rolename)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}
