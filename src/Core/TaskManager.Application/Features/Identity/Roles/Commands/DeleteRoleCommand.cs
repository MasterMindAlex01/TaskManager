using Application.Interfaces.Services.UserAdmon;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.UserAdmon.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<IResult>
    {
        public DeleteRoleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }

    internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IResult>
    {
        private readonly IRoleWriteService _roleWriteService;

        public DeleteRoleCommandHandler(
            IRoleWriteService roleWriteService)
        {
            _roleWriteService = roleWriteService;
        }

        public async Task<IResult> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            return await _roleWriteService.DeleteRole(command.Id, cancellationToken);
        }
    }
}
