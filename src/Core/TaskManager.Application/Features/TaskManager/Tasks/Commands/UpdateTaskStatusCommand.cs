using FluentValidation;
using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Validation;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.TaskManager.Tasks.Commands;

public class UpdateTaskStatusCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Status { get; set; } = null!;
}

internal class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTaskStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var task = await _unitOfWork.Repository<Domain.Tasks.Task>()
                .GetByIdAsync(command.Id);
            if (task == null)
            {
                return await Result<Guid>.FailAsync("Task no found");
            }
            task.UpdateSatatus(command.Status);
            
            await _unitOfWork.Repository<Domain.Tasks.Task>().UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync("Task update");
        }
        catch (Exception)
        {

            return await Result<Guid>.FailAsync("Error");
        }
    }
}

public class UpdateTaskStatusCommandValidator : CustomValidator<UpdateTaskStatusCommand>
{
    public UpdateTaskStatusCommandValidator()
    {
        RuleFor(u => u.Status).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
