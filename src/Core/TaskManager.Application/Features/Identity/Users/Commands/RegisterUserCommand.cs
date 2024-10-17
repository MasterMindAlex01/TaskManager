using MediatR;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Application.Common.Persistence;
using TaskManager.Domain.Common.Events;
using TaskManager.Domain.Identity;
using TaskManager.Domain.Tools;
using TaskManager.Shared.Wrapper;
using Microsoft.Extensions.Configuration;
using TaskManager.Shared.Authorization;
using FluentValidation;
using TaskManager.Application.Common.Validation;

namespace TaskManager.Application.Features.Identity.Users.Commands;

public class RegisterUserCommand : IRequest<Result>
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string RepeatPassword { get; set; } = null!;
    public bool SendPasswordToEmail { get; set; }
}

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly string _pepper;
    private readonly int _iteration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;

    public RegisterUserCommandHandler(
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _pepper = configuration.GetSection("Hash:pepper").Value!;
        _iteration = configuration.GetValue<int>("Hash:iteration");
    }
    public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        try
        {

            string password = string.Empty;
            if (command.SendPasswordToEmail)
            {
                password = PasswordHasher.GenerateSalt(13);
            }
            else
            {
                password = command.Password.Trim();
            }

            User user = User.Create(
                Guid.NewGuid(),
                command.Username,
                command.Firstname,
                command.Lastname,
                command.Email,
                password,
                _pepper,
            _iteration);

            var role = await _roleRepository.GetRoleByNameAsync(ConstantRoles.Employee);
            if (role == null)
            {
                return await Result<Guid>.FailAsync("Error to create user");
            }

            user.AddRole(role);

            // Add Domain Events to be raised after the commit
            user.DomainEvents.Add(EntityCreatedEvent.WithEntity(user));

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync(user.Id, "");
        }
        catch (Exception ex)
        {
            return await Result<Guid>.FailAsync(ex.Message);
        }
    }

}

public class RegisterUserCommandValidator : CustomValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
        .WithMessage("Invalid Email Address.");

        RuleFor(u => u.Username).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(p => p.Firstname).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.Lastname).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(p => p.RepeatPassword).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Equal(p => p.Password);
    }
}
