using TaskManager.Domain.Common.Contracts;
using TaskManager.Domain.Identity;

namespace TaskManager.Domain.Tasks;

public class Comment : BaseEntity, IAggregateRoot
{
    internal Comment(
        Guid id,
        Guid taskId,
        Guid userId,
        string content) : base(id)
    {
        TaskId = taskId;
        UserId = userId;
        Content = content;
        CommentDate = DateTime.UtcNow;
    }

    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public string Content { get; private set; }
    public DateTime CommentDate { get; private set; }

    public virtual Task Task { get; private set; } = default!;
    public virtual User User { get; private set; } = default!;

    public static Comment Create(
        Guid id,
        Guid taskId,
        Guid userId,
        string content,
        DateTime commentDate)
    {
        return new Comment(id, taskId, userId, content);
    }
}
