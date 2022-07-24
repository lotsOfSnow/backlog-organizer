using BacklogOrganizer.Shared.Infrastructure;
using BacklogOrganizer.Shared.Infrastructure.Modularization;

namespace BacklogOrganizer.WebApi;

internal static class Installer
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructure();
        ModuleLoader.Load(services, configuration, AppDomain.CurrentDomain.GetAssemblies());

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
