using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Persistence;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.TaskManager.Tasks.Commands;

public class UpdateTaskCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public int Status { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid AssignedTo { get; set; }
    public string? Tag { get; set; }
}

internal class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public UpdateTaskCommandHandler(
        IUnitOfWork unitOfWork, 
        ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _unitOfWork.Repository<Domain.Tasks.Task>()
                .GetByIdAsync(command.Id);
            if (task == null) 
            {
                return await Result<Guid>.FailAsync("Task no found");
            }

            task.Update(
                command.Title,
                command.Description,
                command.Priority,
                command.Status,
                command.CreationDate,
                _currentUser.GetUserId(),
                command.AssignedTo,
                command.Tag != null ? command.Tag : "none"
                );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync(task.Id, "Task created");
        }
        catch (Exception)
        {

            return await Result<Guid>.FailAsync("Error");
        }
    }
}
