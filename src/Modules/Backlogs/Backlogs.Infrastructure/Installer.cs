using BacklogOrganizer.Modules.Backlogs.Core.Data;
using BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess;
using BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess.Dapper;
using BacklogOrganizer.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Modules.Backlogs.Infrastructure;

internal static class Installer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<BacklogsContext>(configuration);
        services.AddScoped<IBacklogStorage, BacklogsContext>();
        services.AddScoped<IQueryDbConnectionFactory, DapperDbConnectionFactory>();

        return services;
    }
}
