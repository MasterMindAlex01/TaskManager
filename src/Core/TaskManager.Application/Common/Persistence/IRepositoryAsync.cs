using TaskManager.Domain.Common.Contracts;

namespace TaskManager.Application.Common.Persistence;

public interface IRepositoryAsync<T> where T : BaseEntity, IAggregateRoot
{
    IQueryable<T> Entities { get; }

    Task<T?> GetByIdAsync(Guid id);

    Task<List<T>> GetAllAsync();

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
