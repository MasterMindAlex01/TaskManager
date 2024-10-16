
namespace TaskManager.Application.Features.TaskManager.Tasks.Queries;

public class TaskResponse
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public int Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public string AssignedBy { get; set; } = null!;
    public string AssignedTo { get; set; } = null!;
    public string Tag { get; set; } = null!;
}
