using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Roles.Commands;

public class UpdateRoleCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}

internal class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _unitOfWork.Repository<Role>().GetByIdAsync(command.Id);
            if (role == null)
            {
                return await Result<Guid>.FailAsync("Role update failed");
            }

            role.Update(command.Name, command.Description, role.IsDisabled);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(role.Id, "Role updated");
        }
        catch (Exception)
        {
            return await Result<Guid>.FailAsync("Role update failed");
        }

    }
}
