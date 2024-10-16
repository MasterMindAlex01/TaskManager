using MediatR;
using TaskManager.Shared.Wrapper;

namespace Application.Features.UserAdmon.Roles.Commands
{
    public class CreateRoleCommand : IRequest<Result>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

    }

    internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, IResult>
    {
        private readonly IRoleWriteService _roleWriteService;

        public CreateRoleCommandHandler(
            IRoleWriteService roleWriteService)
        {
            _roleWriteService = roleWriteService;
        }

        public async Task<IResult> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            return await _roleWriteService.CreateRole(command, cancellationToken);
        }
    }
}
