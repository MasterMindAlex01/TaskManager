using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Tasks;
using TaskManager.Application.Extensions;
using TaskManager.Application.Features.TaskManager.Tasks.Queries;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Persistence.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly IRepositoryAsync<Domain.Tasks.Task> _repository;

    public TaskRepository(IRepositoryAsync<Domain.Tasks.Task> repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<TaskResponse>> GetAll(int pagenNumber, int pageSize)
    {
        return await _repository.Entities.Select(x => new TaskResponse
        {
            AssignedBy = x.AssignedBy.ToString(),
            AssignedTo = x.AssignedTo.ToString(),
            CreationDate = x.CreationDate,
            Description = x.Description,
            DueDate = x.DueDate,
            Id = x.Id.ToString(),
            Priority = x.Priority,
            Status = x.Status,
            Tag = x.Tag,
            Title = x.Title,
        }).AsNoTracking().ToPaginatedListAsync(pagenNumber, pageSize);
    }

    public async Task<TaskResponse?> GetTaskResponseByIdAsync(Guid id)
    {
        return await _repository.Entities
            .Where(x => x.Id == id)
            .Select(x => new TaskResponse
            {
                AssignedBy = x.AssignedBy.ToString(),
                AssignedTo = x.AssignedTo.ToString(),
                CreationDate = x.CreationDate,
                Description = x.Description,
                DueDate = x.DueDate,
                Id = x.Id.ToString(),
                Priority = x.Priority,
                Status = x.Status,
                Tag = x.Tag,
                Title = x.Title,
            }).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<List<TaskResponse>> GetTaskResponseByAssignedUserIdAsync(Guid userId)
    {
        return await _repository.Entities
            .Where(x => x.AssignedTo == userId)
            .Select(x => new TaskResponse
            {
                AssignedBy = x.AssignedBy.ToString(),
                AssignedTo = x.AssignedTo.ToString(),
                CreationDate = x.CreationDate,
                Description = x.Description,
                DueDate = x.DueDate,
                Id = x.Id.ToString(),
                Priority = x.Priority,
                Status = x.Status,
                Tag = x.Tag,
                Title = x.Title,
            }).AsNoTracking().ToListAsync();
    }

    public async Task<Domain.Tasks.Task?> GetTaskByIdAsync(Guid id)
    {
        return await _repository.Entities
            .Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }
}
