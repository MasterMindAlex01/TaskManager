using TaskManager.Api.Extensions;
using TaskManager.Application.Extensions;
using TaskManager.Infrastructure.Extensions;
using TaskManager.Persistence.Extensions;

namespace TaskManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddConfigurations();

            builder.Services.AddDatabase(builder.Configuration);

            builder.Services.AddApplicationLayer();
            builder.Services.AddRepositories();
            builder.Services.AddServices();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddJwtAuth(builder.Configuration);
            builder.Services.AddCurrentUser();

            var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseCurrentUser();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
