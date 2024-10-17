using FluentValidation;
using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Validation;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.TaskManager.Tasks.Commands;

public class UpdateTaskCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public Guid AssignedTo { get; set; }
    public string? Tag { get; set; }
}

internal class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public UpdateTaskCommandHandler(
        IUnitOfWork unitOfWork, 
        ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _unitOfWork.Repository<Domain.Tasks.Task>()
                .GetByIdAsync(command.Id);
            if (task == null) 
            {
                return await Result<Guid>.FailAsync("Task no found");
            }

            task.Update(
                command.Title,
                command.Description,
                command.Priority,
                command.Status,
                command.CreationDate,
                _currentUser.GetUserId(),
                command.AssignedTo,
                command.Tag != null ? command.Tag : "none"
                );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync("Task update");
        }
        catch (Exception)
        {

            return await Result<Guid>.FailAsync("Error");
        }
    }
}
public class UpdateTaskCommandValidator : CustomValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(u => u.Title).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(u => u.Description).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.Priority).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.CreationDate).Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        RuleFor(p => p.AssignedTo).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}

