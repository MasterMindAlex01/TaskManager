using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;
using TaskManager.Application.Common.Persistence.Tasks;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Features.Identity.Users.Commands;

public class AssignUserTaskCommand : IRequest<Result>
{
    public AssignUserTaskCommand(Guid userId, Guid taskId)
    {
        UserId = userId;
        TaskId = taskId;
    }

    public Guid UserId { get; private set; }
    public Guid TaskId { get; private set; }
}

internal class AssignUserTaskCommandHandler : IRequestHandler<AssignUserTaskCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUser _currentUser;

    public AssignUserTaskCommandHandler(
        IUnitOfWork unitOfWork,
        ITaskRepository taskRepository,
        ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _taskRepository = taskRepository;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(AssignUserTaskCommand command, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _unitOfWork.Repository<User>().GetByIdAsync(command.UserId);
            if (user == null)
            {
                return await Result<Guid>.FailAsync("User not found");
            }

            var task = await _taskRepository.GetTaskByIdAsync(command.TaskId);
            if (task == null)
            {
                return await Result<Guid>.FailAsync("Task not found");
            }

            task.UpdateAssignedUser(_currentUser.GetUserId(), user.Id);

            await _unitOfWork.Repository<Domain.Tasks.Task>().UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync("Task assigned");
        }
        catch (Exception ex)
        {
            return await Result<Guid>.FailAsync(ex.Message);
        }
    }
}

