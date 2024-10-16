using MediatR;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Roles.Queries;

public class GetAllRolesQuery : IRequest<Result<List<RoleResponse>>>
{
}

internal class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
{
    private readonly IRoleRepository _roleRepository;

    public GetAllRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync();
        return await Result<List<RoleResponse>>.SuccessAsync(roles);
    }
}
