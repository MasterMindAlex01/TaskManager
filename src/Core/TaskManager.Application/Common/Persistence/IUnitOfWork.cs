
using TaskManager.Domain.Common.Contracts;

namespace TaskManager.Application.Common.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<T> Repository<T>() where T : BaseEntity, IAggregateRoot;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task CommitTransaction();

        Task Rollback();
    }
}
