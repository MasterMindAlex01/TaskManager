﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Persistence.Context;

namespace TaskManager.Api.Extensions;

public static class ServiceCollectionExtensions
{
    internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
        return builder;
    }

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

    internal static IServiceCollection AddJwtAuth(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                    }
                });
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //c.IncludeXmlComments(xmlPath);
        });

        services
        .AddHttpContextAccessor()
        .AddAuthorization()
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
                    )
            };
        });

        return services;
    }
}
