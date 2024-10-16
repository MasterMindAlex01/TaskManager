using TaskManager.Application.Features.TaskManager.Tasks.Queries;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Common.Persistence.Tasks;

public interface ITaskRepository
{
    Task<PaginatedResult<TaskResponse>> GetAll(int pagenNumber, int pageSize);
    Task<TaskResponse?> GetTaskByIdAsync(Guid id);
}
