using TaskManager.Domain.Identity;

namespace TaskManager.Application.Common.Persistence.Roles;

public interface IRoleRepository
{
    Task<List<Role>> GetRoleListByIdListAsync(List<Guid> roleIdList);
}
