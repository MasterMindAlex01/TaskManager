using Application.Interfaces.Services.UserAdmon;
using Application.Models.Responses;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.UserAdmon.Roles.Queries
{
    public class GetAllRolesQuery : IRequest<Result<List<RoleResponse>>>
    {
    }

    internal class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
    {
        private readonly IRoleReadService _roleReadPlatformService;

        public GetAllRolesQueryHandler(IRoleReadService roleReadPlatformService)
        {
            _roleReadPlatformService = roleReadPlatformService;
        }

        public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleReadPlatformService.RetrieveAll();
        }
    }
}
