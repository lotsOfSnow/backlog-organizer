using BacklogOrganizer.Shared.Core.Domain.Entities.Identity;
using BacklogOrganizer.Shared.Core.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Core;

internal static class Installer
{
    public static IServiceCollection AddSharedCore(this IServiceCollection services)
    {
        services.AddMediatR();
        services.AddSingleton<IIdProvider<Guid>, GuidIdProvider>();

        return services;
    }
}
