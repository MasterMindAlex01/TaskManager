using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Roles.Commands;

public class DeleteRoleCommand : IRequest<Result>
{
    public DeleteRoleCommand(Guid id) => Id = id;

    public Guid Id { get; set; }

}

internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleRepository.GetRoleByIdWithUsersAsync(command.Id);
            if (role == null)
            {
                return await Result<Guid>.FailAsync("Role delete failed");
            }

            if (role.Users.Count > 0)
            {
                return await Result<Guid>.FailAsync("error.msg.role.associated.with.users.deleted");
            }

            await _unitOfWork.Repository<Role>().DeleteAsync(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(role.Id, "Role deleted");
        }
        catch (Exception)
        {
            return await Result<Guid>.FailAsync("Role creation failed");
        }
    }
}
