using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Application.Common.Validation;
using TaskManager.Domain.Tools;
using TaskManager.Shared.Wrapper;

namespace TaskManager.Application.Features.Identity.Tokens.Queries.GetToken;

public class GetTokenQuery : IRequest<Result<TokenResponse>>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

internal class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, Result<TokenResponse>>
{
    private string _pepper;
    private readonly int _iteration;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public GetTokenQueryHandler(
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _pepper = configuration.GetSection("Hash:pepper").Value!;
        _iteration = configuration.GetValue<int>("Hash:iteration");
    }

    public async Task<Result<TokenResponse>> Handle(GetTokenQuery query, CancellationToken cancellationToken)
    {
        // Verificamos credenciales con Identity
        var user = await _userRepository.GetUserByUsernameWithRolesAsync(query.UserName);
        if (user == null)
        {
            return await Result<TokenResponse>.FailAsync("User or password incorrect!");
        }

        var passwordHash = PasswordHasher.ComputeHash(
            query.Password, user.PasswordSalt, _pepper, _iteration);
        if (user.Password != passwordHash) return await Result<TokenResponse>.FailAsync("User or password incorrect!");

        // Generamos un token según los claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.Firstname),
            new Claim(ClaimTypes.Surname, user.Lastname),
        };

        var roles = user.Roles.Select(x => x.Role);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name.ToString()));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return await Result<TokenResponse>.SuccessAsync(new TokenResponse()
        {
            AccessToken = jwt
        }, "Ok");
    }
}

public class GetTokenQueryValidator : CustomValidator<GetTokenQuery>
{
    public GetTokenQueryValidator()
    {
        RuleFor(u => u.UserName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(u => u.Password).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(6);
    }
}
