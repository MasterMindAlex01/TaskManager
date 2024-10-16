using Application.Interfaces.Services.UserAdmon;
using Application.Models.Responses;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.UserAdmon.Roles.Queries
{
    public class GetRoleWithPermisionsByIdQuery : IRequest<Result<RolePermissionsResponse>>
    {
        public GetRoleWithPermisionsByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }

    internal class GetRoleWithPermisionsByIdQueryHandler : IRequestHandler<GetRoleWithPermisionsByIdQuery, Result<RolePermissionsResponse>>
    {
        private readonly IRoleReadService _roleReadPlatformService;

        public GetRoleWithPermisionsByIdQueryHandler(IRoleReadService roleReadPlatformService)
        {
            _roleReadPlatformService = roleReadPlatformService;
        }

        public async Task<Result<RolePermissionsResponse>> Handle(GetRoleWithPermisionsByIdQuery query, CancellationToken cancellationToken)
        {
            return await _roleReadPlatformService.RetrieveOneWithPermisions(query.Id);
        }
    }
}
