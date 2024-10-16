using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Persistence.Repository;
using TaskManager.Persistence.Repository.Roles;
using TaskManager.Persistence.Repository.Users;

namespace TaskManager.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        return services;
    }
}
