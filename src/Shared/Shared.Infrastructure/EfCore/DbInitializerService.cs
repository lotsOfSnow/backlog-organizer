using BacklogOrganizer.Shared.Core.Reflection;
using BacklogOrganizer.Shared.Core.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BacklogOrganizer.Shared.Infrastructure.EfCore;

public class DbInitializerService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DbInitializerService> _logger;

    public DbInitializerService(IServiceProvider serviceProvider, ILogger<DbInitializerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextsTypes = Assemblies.GetAllNonDynamic()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsConcreteImplementationOf<DbContext>());

        await using var sp = _serviceProvider.CreateAsyncScope();
        foreach (var type in dbContextsTypes)
        {
            var dbContext = (DbContext)sp.ServiceProvider.GetRequiredService(type);

            _logger.LogInformation("Recreating database for context {DbContext}", type);

            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
            await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
