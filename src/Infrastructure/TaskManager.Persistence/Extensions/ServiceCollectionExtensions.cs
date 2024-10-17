using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Common.Persistence;
using TaskManager.Application.Common.Persistence.Roles;
using TaskManager.Application.Common.Persistence.Tasks;
using TaskManager.Application.Common.Persistence.Users;
using TaskManager.Persistence.Context;
using TaskManager.Persistence.Repository;

namespace TaskManager.Persistence.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
                    .AddDbContext<ApplicationDbContext>(options => options
                        .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                    );
    }
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
