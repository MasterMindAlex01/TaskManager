using TaskManager.Application.Features.TaskManager.Tasks.Queries;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Common.Persistence.Tasks;

public interface ITaskRepository
{
    Task<PaginatedResult<TaskResponse>> GetAll(int pagenNumber, int pageSize);
    Task<TaskResponse?> GetTaskResponseByIdAsync(Guid id);
    Task<Domain.Tasks.Task?> GetTaskByIdAsync(Guid id);
    Task<List<TaskResponse>> GetTaskResponseByAssignedUserIdAsync(Guid userId);
}
