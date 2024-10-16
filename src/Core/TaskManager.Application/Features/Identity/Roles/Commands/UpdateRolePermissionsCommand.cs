using Application.Interfaces.Services.UserAdmon;
using Application.Models.Requests;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.UserAdmon.Roles.Commands
{
    public class UpdateRolePermissionsCommand : IRequest<IResult>
    {
        public required Guid RoleId { get; set; }
        public required IEnumerable<PermissionRequest> Permissions { get; set; } = new List<PermissionRequest>();
    }

    internal class UpdateRolePermissionsCommandHandler : IRequestHandler<UpdateRolePermissionsCommand, IResult>
    {
        private readonly IRoleWriteService _roleWriteService;

        public UpdateRolePermissionsCommandHandler(
            IRoleWriteService roleWriteService)
        {
            _roleWriteService = roleWriteService;
        }

        public async Task<IResult> Handle(UpdateRolePermissionsCommand command, CancellationToken cancellationToken)
        {
            return await _roleWriteService.UpdateRolePermissions(command.RoleId, command, cancellationToken);
        }
    }
}
