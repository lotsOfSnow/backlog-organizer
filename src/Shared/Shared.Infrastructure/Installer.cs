using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure;

internal static class Installer
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
