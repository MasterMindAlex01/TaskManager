using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManager.Infrastructure.Extensions;
using TaskManager.Infrastructure.Middleware;
using TaskManager.Infrastructure.Validations;
using TaskManager.Persistence.Extensions;

namespace TaskManager.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(TaskManager.Application.AssemblyReference).GetTypeInfo().Assembly;
        return services
            .AddExceptionMiddleware()
            .AddBehaviours(applicationAssembly)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddRequestLogging(config)
            .AddDatabase(config)
            .AddRepositories()
            .AddRouting(options => options.LowercaseUrls = true)
            .AddServices();
    }
    
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
        builder
            .UseStaticFiles()
            .UseExceptionMiddleware()
            .UseRouting()
            .UseAuthentication()
            .UseCurrentUser()
            .UseAuthorization()
            .UseRequestLogging(config);

}
