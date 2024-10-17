using FluentValidation;
using MediatR;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Validation;
using TaskManager.Domain.Identity;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Roles.Commands;

public class CreateRoleCommand : IRequest<Result>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

}

internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var role = Role.Create(Guid.NewGuid(), command.Name, command.Description);

            await _unitOfWork.Repository<Role>().AddAsync(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(role.Id, "Role Created");
        }
        catch (Exception)
        {
            return await Result<Guid>.FailAsync("Role creation failed");
        }
    }
}

public class CreateRoleCommandValidator : CustomValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {

        RuleFor(u => u.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(p => p.Description).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}

