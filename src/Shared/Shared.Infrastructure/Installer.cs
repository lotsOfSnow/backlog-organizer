using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure;

internal static class Installer
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}
