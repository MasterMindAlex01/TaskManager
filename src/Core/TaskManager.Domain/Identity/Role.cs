using TaskManager.Domain.Common.Contracts;

namespace TaskManager.Domain.Identity;

public class Role : BaseEntity, IAggregateRoot
{
    private readonly List<UserRole> _users = [];

    internal Role(
        Guid id,
        string name,
        string description,
        bool isDisabled) : base(id)
    {
        Name = name;
        Description = description;
        IsDisabled = isDisabled;
    }

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public bool IsDisabled { get; private set; }

    public virtual IReadOnlyCollection<UserRole> Users => _users;

    public static Role Create(
        Guid id,
        string name,
        string description)
    {
        return new Role(id, name, description, false);
    }

    public void Update(
        string name,
        string description,
        bool isDisabled)
    {
        Name = name;
        Description = description;
        IsDisabled = isDisabled;
    }
}
