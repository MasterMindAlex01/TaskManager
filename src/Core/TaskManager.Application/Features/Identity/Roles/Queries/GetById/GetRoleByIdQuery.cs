using Application.Interfaces.Services.UserAdmon;
using Application.Models.Responses;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.UserAdmon.Roles.Queries
{
    public class GetRoleByIdQuery : IRequest<Result<RoleResponse>>
    {
        public GetRoleByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }

    internal class GetRoleByIdQueryQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleResponse>>
    {
        private readonly IRoleReadService _roleReadService;

        public GetRoleByIdQueryQueryHandler(IRoleReadService roleReadService)
        {
            _roleReadService = roleReadService;
        }

        public async Task<Result<RoleResponse>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
        {
            return await _roleReadService.RetrieveOne(query.Id);
        }
    }
}
