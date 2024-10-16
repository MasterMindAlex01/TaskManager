using MediatR;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Roles.Queries
{
    public class GetRoleByIdQuery : IRequest<Result<RoleResponse?>>
    {
        public GetRoleByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }

    internal class GetRoleByIdQueryQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleResponse?>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleByIdQueryQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result<RoleResponse?>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleByIdAsync(query.Id);
            return await Result<RoleResponse?>.SuccessAsync(role);
        }
    }
}
