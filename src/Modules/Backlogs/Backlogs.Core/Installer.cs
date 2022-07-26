using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Modules.Backlogs.Core;

internal static class Installer
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(typeof(Installer).Assembly);

        services.AddScoped<IBacklogRepository, BacklogRepository>();

        return services;
    }
}
