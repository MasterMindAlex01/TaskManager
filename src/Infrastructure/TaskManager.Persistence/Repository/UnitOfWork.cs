using System.Collections;
using TaskManager.Application.Common.Events;
using TaskManager.Application.Common.Persistence;
using TaskManager.Domain.Common.Contracts;
using TaskManager.Persistence.Context;

namespace TaskManager.Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = null!;
    private readonly IEventPublisher _events;
    private bool disposed;
    private Hashtable _repositories = null!;

    public UnitOfWork(ApplicationDbContext dbContext, IEventPublisher events)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _events = events;
    }

    public IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : BaseEntity, IAggregateRoot
    {
        if (_repositories == null)
            _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryAsync<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
            _repositories.Add(type, repositoryInstance);
        }

        return (IRepositoryAsync<TEntity>)_repositories[type]!;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        int result = await _dbContext.SaveChangesAsync(cancellationToken);

        await SendDomainEventsAsync();

        return result;
    }

    public Task CommitTransaction()
    {
        return _dbContext.Database.CommitTransactionAsync();
    }


    public Task Rollback()
    {
        return _dbContext.Database.RollbackTransactionAsync();
    }

    private async Task SendDomainEventsAsync()
    {
        var entitiesWithEvents = _dbContext.ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var domainEvents = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach (var domainEvent in domainEvents)
            {
                await _events.PublishAsync(domainEvent);
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                _dbContext.Dispose();
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }
}
