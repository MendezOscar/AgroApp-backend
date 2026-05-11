using AgroApp.Application.Common.Interfaces;
using AgroApp.Infrastructure.Identity;
using AgroApp.Infrastructure.Persistence;
using AgroApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgroApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AgroAppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsql => npgsql.MigrationsAssembly(
                    typeof(AgroAppDbContext).Assembly.FullName)
            )
            .UseSnakeCaseNamingConvention());

        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<AgroAppDbContext>());

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IStorageService, R2StorageService>();
        services.AddScoped<INotificationService, FcmNotificationService>();

        return services;
    }
}