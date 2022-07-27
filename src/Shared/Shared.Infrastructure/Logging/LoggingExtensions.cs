using BacklogOrganizer.Shared.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BacklogOrganizer.Shared.Infrastructure.Logging;

public static class LoggingExtensions
{
    /// <param name="configure">Use to override the sinks. Will use Seq if this is null.</param>
    public static IServiceCollection UseSerilog(this ILoggingBuilder loggingBuilder, IServiceCollection services, IConfiguration configuration, Action<LoggerConfiguration>? configure = null)
    {
        var loggerConfig = new LoggerConfiguration()
            .Enrich.FromLogContext();
        ConfigureSinks(loggerConfig, services, configuration, configure);

        var logger = Log.Logger = loggerConfig.CreateLogger();
        loggingBuilder.AddSerilog(logger, true);

        return services;
    }

    private static void ConfigureSinks(LoggerConfiguration config, IServiceCollection services, IConfiguration configuration, Action<LoggerConfiguration>? configure)
    {
        if (configure is not null)
        {
            configure(config);
            return;
        }

        var options = services.ConfigureOptions<SeqOptions>(configuration, SeqOptions.SectionName);
        config.WriteTo.Seq(options.ServerUrl);
    }
}
