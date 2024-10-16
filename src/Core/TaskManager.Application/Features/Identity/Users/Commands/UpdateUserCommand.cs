using MediatR;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Application.Common.Persistence;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Commands;

public class UpdateUserCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public bool Enabled { get; set; }
    public bool SendPasswordToEmail { get; set; }
    public List<Guid> Roles { get; set; } = [];

}

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            User? currentAppuser = await _userRepository.GetByIdWithRolesAsync(command.Id);
            if (currentAppuser == null || currentAppuser.IsDeleted)
            {
                return await Result<Guid>.FailAsync($"User with ID {command.Id} does not exist");
            }

            currentAppuser.Update(
                command.Username,
                command.Firstname,
                command.Lastname,
                command.Email,
                command.Enabled);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync(currentAppuser.Id, "User deleted");
        }
        catch (Exception)
        {

            return await Result<Guid>.FailAsync("error");
        }

    }
}
