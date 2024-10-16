using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Domain.Common.Events;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Commands;

public class AssignUserRolesCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIdList { get; set; } = [];
}

internal class AssignUserRolesCommandHandler : IRequestHandler<AssignUserRolesCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public AssignUserRolesCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Handle(AssignUserRolesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByIdWithRolesAsync(command.UserId);
            if (user == null) 
            {
                return await Result<Guid>.FailAsync("user not found");
            }

            List<Role> roles = await _roleRepository.GetRoleListByIdListAsync(command.RoleIdList);
            if (roles == null || !roles.Any())
            {
                return await Result<Guid>.FailAsync("roles not found");
            }

            if (roles.Count > 0)
            {
                user.AddRoleRange(roles);
            }

            // Add Domain Events to be raised after the commit
            user.DomainEvents.Add(EntityCreatedEvent.WithEntity(user));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync(user.Id, "");
        }
        catch (Exception ex)
        {
            return await Result<Guid>.FailAsync(ex.Message);
        }
    }
}