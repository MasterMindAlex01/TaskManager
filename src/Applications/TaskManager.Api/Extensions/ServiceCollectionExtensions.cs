using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Persistence.Context;

namespace TaskManager.Api.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddDatabase(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        return services
                    .AddDbContext<ApplicationDbContext>(options => options
                        .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                    );
    }
}
