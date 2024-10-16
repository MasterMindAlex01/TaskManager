using MediatR;
using System.ComponentModel.DataAnnotations;
using TaskManager.Shared.Wrapper;

namespace Application.Features.UserAdmon.Roles.Commands;

public class UpdateRoleCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public required string name { get; set; } = null!;
    public required string Description { get; set; }
}

internal class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
{
    private readonly IRoleWriteService _roleWriteService;

    public UpdateRoleCommandHandler(
        IRoleWriteService roleWriteService)
    {
        _roleWriteService = roleWriteService;
    }

    public async Task<Result> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        return await _roleWriteService.UpdateRole(command.Id, command, cancellationToken);

    }
}
