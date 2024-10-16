using MediatR;
using TaskManager.Application.Common.Persistence.Tasks;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.TaskManager.Tasks.Queries;

public class GetAllTasksQuery : IRequest<PaginatedResult<TaskResponse>>
{
    public GetAllTasksQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
}

internal class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, PaginatedResult<TaskResponse>>
{
    private readonly ITaskRepository _taskRepository;

    public GetAllTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<PaginatedResult<TaskResponse>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetAll(query.PageNumber, query.PageSize);
    }
}