using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Domain.Identity;

namespace TaskManager.Persistence.Repository.Roles;

public class RoleRepository : IRoleRepository
{
    private readonly IRepositoryAsync<Role> _repository;

    public RoleRepository(IRepositoryAsync<Role> repository)
    {
        _repository = repository;
    }

    public async Task<List<Role>> GetRoleListByIdListAsync(List<Guid> roleIdList)
    {
        return await _repository.Entities
            .Where(x => roleIdList.Contains(x.Id))
            .AsNoTracking()
            .ToListAsync();
    }
}
