using BacklogOrganizer.Modules.Backlogs.Core;
using BacklogOrganizer.Modules.Backlogs.Infrastructure;
using BacklogOrganizer.Shared.Infrastructure.Modularization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Modules.Backlogs.Module;

public class BacklogsModule : IModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore();
        services.AddInfrastructure(configuration);
    }
}
