using TaskManager.Domain.Common.Contracts;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Tasks;

public class Task : BaseEntity, IAggregateRoot
{
    private readonly List<Comment> _comments = [];
    private readonly List<StatusHistory> _statusHistories = [];

    internal Task(
        Guid id,
        string title,
        string description,
        string priority,
        Guid assignedBy,
        Guid assignedTo,
        string tag) : base(id)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Status = ETaskStatus.Pending.ToString();
        CreationDate = DateTime.UtcNow;
        DueDate = DateTime.UtcNow.AddMonths(1);
        AssignedBy = assignedBy;
        AssignedTo = assignedTo;
        Tag = tag;
    }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Priority { get; private set; } = null!;
    public string Status { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public Guid AssignedBy { get; private set; }
    public Guid AssignedTo { get; private set; }
    public string Tag { get; private set; }

    public virtual IReadOnlyCollection<Comment> Comments => _comments;
    public virtual IReadOnlyCollection<StatusHistory> StatusHistories => _statusHistories;

    public static Task Create(
        Guid id,
        string title,
        string description,
        string priority,
        Guid assignedBy,
        Guid assignedTo,
        string tag)
    {
        return new Task(id, title, description, priority, assignedBy, assignedTo, tag);
    }

    public void UpdateAssignedUser(Guid assignedBy, Guid assignedTo)
    {
        AssignedBy = assignedBy;
        AssignedTo = assignedTo;
    }

    public void UpdateSatatus(string status)
    {
        Status = status;
    }

    public void Update(
        string title,
        string description,
        string priority,
        string status,
        DateTime creationDate,
        Guid assignedBy,
        Guid assignedTo,
        string tag)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Status = status;
        CreationDate = creationDate;
        DueDate = creationDate.AddMonths(1);
        AssignedBy = assignedBy;
        AssignedTo = assignedTo;
        Tag = tag;
    }
}
