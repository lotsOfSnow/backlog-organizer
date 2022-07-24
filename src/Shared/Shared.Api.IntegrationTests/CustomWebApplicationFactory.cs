using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Api.IntegrationTests;

public abstract class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    private readonly Type? _dbContextType;
    private readonly string? _configurationFileName;

    protected CustomWebApplicationFactory(string? configurationFileName = "appsettings.test.json", Type? dbContextType = null)
    {
        _configurationFileName = configurationFileName;
        _dbContextType = dbContextType;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            if (_dbContextType is not null)
            {
                RecreateDatabase(services);
            }
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

    private void RecreateDatabase(IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        if (scope.ServiceProvider.GetRequiredService(_dbContextType!) is not DbContext db)
        {
            throw new InvalidOperationException($"Provided type '{_dbContextType}' can't be used as a DbContext");
        }

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}
