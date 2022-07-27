using BacklogOrganizer.Shared.Core;
using BacklogOrganizer.Shared.Infrastructure;
using BacklogOrganizer.Shared.Infrastructure.Logging;
using BacklogOrganizer.Shared.Infrastructure.Modularization;

namespace BacklogOrganizer.WebApi;

internal static class Installer
{
    public static WebApplicationBuilder AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddSharedCore();
        builder.Services.AddSharedInfrastructure();

        builder.Logging.UseSerilog(builder.Services, builder.Configuration);

        ModuleLoader.Load(builder.Services, builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}
