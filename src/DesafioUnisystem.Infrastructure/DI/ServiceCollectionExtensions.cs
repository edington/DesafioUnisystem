using DesafioUnisystem.ApplicationService.Interface;
using DesafioUnisystem.ApplicationService.Service;
using DesafioUnisystem.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioUnisystem.Infrastructure.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null)));

        services.AddScoped<IUserRepository, UserRepository>();


        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
         services.AddScoped<IUserService, UserService>();
         services.AddScoped<IAuthService,AuthService>();

        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
       {
           options.AssumeDefaultVersionWhenUnspecified = true;
           options.DefaultApiVersion = new ApiVersion(1, 0);
           options.ReportApiVersions = true;

           options.ApiVersionReader = new UrlSegmentApiVersionReader();
       });

        return services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV"; // Formato: v1, v2, etc.
            options.SubstituteApiVersionInUrl = true; // Substitui {version}
        });
    }
}
