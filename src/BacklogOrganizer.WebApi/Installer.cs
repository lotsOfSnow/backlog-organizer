using BacklogOrganizer.Shared.Core;
using BacklogOrganizer.Shared.Infrastructure;
using BacklogOrganizer.Shared.Infrastructure.Logging;
using BacklogOrganizer.Shared.Infrastructure.Modularization;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace BacklogOrganizer.WebApi;

internal static class Installer
{
    public static WebApplicationBuilder AddDependencies(this WebApplicationBuilder builder)
    {
        ModuleLoader.Load(builder.Services, builder.Configuration);

        builder.Services.AddSharedCore();
        builder.Services.AddSharedInfrastructure();

        builder.Logging.UseSerilog(builder.Services, builder.Configuration);

        builder.Services.SetupVersioning();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

        return builder;
    }

    private static IServiceCollection SetupVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(x =>
        {
            x.AssumeDefaultVersionWhenUnspecified = true;
            x.DefaultApiVersion = new(1, 0);
            x.ReportApiVersions = true;
            x.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddVersionedApiExplorer(x =>
        {
            x.GroupNameFormat = "'v'VVV";
            x.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
