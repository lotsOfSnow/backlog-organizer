using BacklogOrganizer.Modules.Backlogs.Core.Gaming;
using BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess;
using BacklogOrganizer.Shared.Api.IntegrationTests;
using BacklogOrganizer.Shared.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests;

public sealed class BacklogsApplicationFactory : CustomWebApplicationFactory<Program>
{
    public async Task<Backlog> GetNewBacklogAsync()
    {
        await using var scope = Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<BacklogsContext>();

        var backlog = new Backlog();
        db.Backlogs.Add(backlog);
        await db.SaveChangesAsync();

        return backlog;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        await using var scope = Services.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(request);
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : EntityBase
    {
        await using var scope = Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<BacklogsContext>();
        return await context.FindAsync<TEntity>(keyValues);
    }

    public async Task SaveChangesAsync()
    {
        await using var scope = Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<BacklogsContext>();

        await db.SaveChangesAsync();
    }

    public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        await using var scope = Services.CreateAsyncScope();

        //var dbContext = scope.ServiceProvider.GetRequiredService<BacklogsContext>();

        try
        {
            // TODO: Uncomment once transactions are used.
            //await dbContext.Database.BeginTransactionAsync();

            await action(scope.ServiceProvider);

            //await dbContext.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            //await dbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public Task ExecuteDbContextAsync(Func<BacklogsContext, Task> action)
    {
        return ExecuteScopeAsync(sp => action(sp.GetRequiredService<BacklogsContext>()));
    }
}
