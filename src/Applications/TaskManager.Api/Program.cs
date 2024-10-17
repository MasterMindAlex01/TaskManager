using TaskManager.Api.Extensions;
using TaskManager.Application.Extensions;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Extensions;
using TaskManager.Infrastructure.Logging.Serilog;

namespace TaskManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddConfigurations().RegisterSerilog();
            
            builder.Services.AddControllers();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplicationLayer();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddJwtAuth(builder.Configuration);
            builder.Services.AddCurrentUser();


            var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseInfrastructure(builder.Configuration);

            //app.UseAuthentication();
            //app.UseCurrentUser();
            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
