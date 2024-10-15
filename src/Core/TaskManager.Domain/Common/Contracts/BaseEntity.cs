using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManager.Domain.Common.Contracts;

public abstract class BaseEntity : BaseEntity<Guid>
{
    protected BaseEntity(Guid id) => Id = id;
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; protected set; } = default!;

    [NotMapped]
    public List<DomainEvent> DomainEvents { get; } = [];
}