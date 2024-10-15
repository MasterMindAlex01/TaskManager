using TaskManager.Domain.Common.Contracts;
using TaskManager.Domain.Identity;

namespace TaskManager.Domain.Tasks;

public class StatusHistory : BaseEntity, IAggregateRoot
{
    internal StatusHistory(
        Guid id,
        Guid taskId,
        string previousStatus,
        string newStatus,
        Guid userId) :base(id)
    {
        TaskId = taskId;
        PreviousStatus = previousStatus;
        NewStatus = newStatus;
        ChangeDate = DateTime.UtcNow;
        UserId = userId;
    }

    public Guid TaskId { get; private set; }
    public string PreviousStatus { get; private set; } = null!;
    public string NewStatus { get; private set; } = null!;
    public DateTime ChangeDate { get; private set; }
    public Guid UserId { get; private set; }

    public virtual Task Task { get; private set; } = default!;
    public virtual User User { get; private set; } = default!;

    public static StatusHistory Create(
        Guid id,
        Guid taskId,
        string previousStatus,
        string newStatus,
        Guid userId)
    {
        return new StatusHistory(id, taskId, previousStatus, newStatus, userId);
    }

}
