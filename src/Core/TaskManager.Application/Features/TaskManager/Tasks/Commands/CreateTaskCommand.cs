using FluentValidation;
using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Validation;
using TaskManager.Application.Features.Identity.Users.Commands;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.TaskManager.Tasks.Commands;

public class CreateTaskCommand : IRequest<Result>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public Guid AssignedTo { get; set; }
    public string? Tag { get; set; }
}

internal class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public CreateTaskCommandHandler(
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var task = Domain.Tasks.Task.Create(
            Guid.NewGuid(),
            command.Title,
            command.Description,
            command.Priority,
            _currentUser.GetUserId(),
            command.AssignedTo,
            command.Tag != null ? command.Tag : "none"
            );

            await _unitOfWork.Repository<Domain.Tasks.Task>().AddAsync(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync(task.Id, "Task created");
        }
        catch (Exception)
        {

            return await Result<Guid>.FailAsync("Error");
        }
    }
}

public class CreateTaskCommandValidator : CustomValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(u => u.Title).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(u => u.Description).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.Priority).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.AssignedTo).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
