using BacklogOrganizer.Shared.Infrastructure.EfCore;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure;

internal static class Installer
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddHostedService<DbInitializerService>();

        return services;
    }
}
