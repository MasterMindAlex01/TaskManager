using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Application.Common.Persistence.Tasks;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Persistence.Repository;

namespace TaskManager.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        return services;
    }
}
