using MediatR;
using TaskManager.Application.Common.Persistence.Tasks;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.TaskManager.Tasks.Queries;

public class GetTaskByIdQuery : IRequest<Result<TaskResponse?>>
{
    public GetTaskByIdQuery(Guid id) => Id = id;
    public Guid Id { get; private set; }
}

internal class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskResponse?>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Result<TaskResponse?>> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskByIdAsync(query.Id);
        return await Result<TaskResponse?>.SuccessAsync(task);
    }
}

