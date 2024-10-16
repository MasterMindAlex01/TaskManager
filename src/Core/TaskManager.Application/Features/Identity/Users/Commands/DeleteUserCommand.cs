using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Domain.Common.Events;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Commands;

public class DeleteUserCommand : IRequest<Result>
{
    public DeleteUserCommand(Guid id) => Id = id;

    public Guid Id { get; set; }

}

internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            User? currentUser = await _userRepository.GetUserByIdWithRolesAsync(command.Id);
            if (currentUser == null || currentUser.IsDeleted)
            {
                return await Result<Guid>.FailAsync($"User with ID {command.Id} does not exist");
            }

            //if (_userRepository.IsSystemUser(currentAppuser.Username))
            //{
            //    return await Result<long>.FailAsync("User configured as the system user cannot be deleted");
            //}

            // Add Domain Events to be raised after the commit
            currentUser.DomainEvents.Add(EntityDeletedEvent.WithEntity(currentUser));

            currentUser.Delete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync(currentUser.Id, "User deleted");
        }
        catch (Exception)
        {

            return await Result<Guid>.FailAsync("error");
        }
    }
}
