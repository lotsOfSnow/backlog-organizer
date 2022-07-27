using BacklogOrganizer.Shared.Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Serilog;
using Xunit.Abstractions;

namespace BacklogOrganizer.Shared.Api.IntegrationTests;

public abstract class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    private readonly Type? _dbContextType;
    private readonly string? _configurationFileName;

    public ITestOutputHelper? TestOutputHelper { get; set; }

    protected CustomWebApplicationFactory(string? configurationFileName = "appsettings.test.json")
    {
        _configurationFileName = configurationFileName;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging
                .UseSerilog(logging.Services, new ConfigurationManager(), conf => conf.WriteTo.TestOutput(TestOutputHelper));
        });

        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            if (_configurationFileName is not null)
            {
                AddConfiguration(configurationBuilder);
            }
        });
    }

    private void AddConfiguration(IConfigurationBuilder configurationBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables()
            .Build();

        configurationBuilder.AddConfiguration(config);
    }
}
