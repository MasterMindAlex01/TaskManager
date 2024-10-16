using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.TaskManager.Tasks.Commands;

public class DeleteTaskCommand : IRequest<Result>
{
    public DeleteTaskCommand(Guid id) => Id = id;
    public Guid Id { get; private set; }
}

internal class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _unitOfWork.Repository<Domain.Tasks.Task>()
                .GetByIdAsync(command.Id);
            if (task == null)
            {
                return await Result<Guid>.FailAsync("Task no found");
            }

            await _unitOfWork.Repository<Domain.Tasks.Task>().DeleteAsync(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync(task.Id, "Task deleted");
        }
        catch (Exception)
        {

            return await Result<Guid>.FailAsync("Error");
        }
    }
}

