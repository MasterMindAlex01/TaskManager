using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Application.Common.Validation;
using TaskManager.Domain.Common.Events;
using TaskManager.Domain.Identity;
using TaskManager.Domain.Tools;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Users.Commands;

public class CreateUserCommand : IRequest<Result>
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool SendPasswordToEmail { get; set; }
    public List<Guid> Roles { get; set; } = [];

}

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private string _pepper;
    private readonly int _iteration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;

    public CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _pepper = configuration.GetSection("Hash:pepper").Value!;
        _iteration = configuration.GetValue<int>("Hash:iteration");
    }

    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            List<Role> roles = await _roleRepository.GetRoleListByIdListAsync(command.Roles);
            if (roles == null || !roles.Any())
            {
                return await Result<Guid>.FailAsync("roles not found");
            }

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

            if (roles.Count > 0)
            {
                user.AddRoleRange(roles);
            }

            // Add Domain Events to be raised after the commit
            user.DomainEvents.Add(EntityCreatedEvent.WithEntity(user));

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (command.SendPasswordToEmail)
            {
                //enviar correos
            }

            return await Result<Guid>.SuccessAsync(user.Id, "");
        }
        catch (Exception ex)
        {
            return await Result<Guid>.FailAsync(ex.Message);
        }
    }
}

public class CreateUserCommandValidator : CustomValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
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

        RuleFor(p => p.Roles).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
