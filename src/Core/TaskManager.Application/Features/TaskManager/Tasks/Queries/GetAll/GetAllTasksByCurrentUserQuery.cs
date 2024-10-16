using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Persistence.Tasks;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.TaskManager.Tasks.Queries.GetAll;

public class GetAllTasksByCurrentUserQuery : IRequest<Result<List<TaskResponse>>>
{
}

internal class GetAllTasksByCurrentUserQueryHandler : IRequestHandler<GetAllTasksByCurrentUserQuery, Result<List<TaskResponse>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICurrentUser _currentUser;

    public GetAllTasksByCurrentUserQueryHandler(
        ITaskRepository taskRepository,
        ICurrentUser currentUser)
    {
        _taskRepository = taskRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<List<TaskResponse>>> Handle(GetAllTasksByCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var taskList = await _taskRepository.GetTaskResponseByAssignedUserIdAsync(_currentUser.GetUserId());
        return await Result<List<TaskResponse>>.SuccessAsync(taskList);
    }
}
