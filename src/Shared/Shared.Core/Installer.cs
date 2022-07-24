using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Core;

internal static class Installer
{
    public static IServiceCollection AddSharedCore(this IServiceCollection services)
    {
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
