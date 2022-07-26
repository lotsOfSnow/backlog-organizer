using BacklogOrganizer.Shared.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BacklogOrganizer.Shared.Infrastructure.Logging;

public static class LoggingExtensions
{
    public static IServiceCollection UseSerilog(this ILoggingBuilder loggingBuilder, IServiceCollection services, IConfiguration configuration)
    {
        var options = services.ConfigureOptions<SeqOptions>(configuration, SeqOptions.SectionName);

        var logger = Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Seq(options.ServerUrl)
            .CreateLogger();

        loggingBuilder.AddSerilog(logger, true);

        return services;
    }
}
