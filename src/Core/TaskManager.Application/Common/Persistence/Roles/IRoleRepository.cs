using TaskManager.Application.Features.Identity.Roles.Queries;
using TaskManager.Domain.Identity;

namespace TaskManager.Application.Common.Persistence.Roles;

public interface IRoleRepository
{
    Task<List<Role>> GetRoleListByIdListAsync(List<Guid> roleIdList);
    Task<Role?> GetRoleByIdWithUsersAsync(Guid roleId);
    Task<Role?> GetRoleByNameAsync(string rolename);
    Task<RoleResponse?> GetRoleByIdAsync(Guid roleId);
    Task<List<RoleResponse>> GetAllAsync();

}
