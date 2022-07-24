using BacklogOrganizer.Modules.Backlogs.Infrastructure.DataAccess;
using BacklogOrganizer.Shared.Api.IntegrationTests;
using BacklogOrganizer.Shared.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Modules.Backlogs.Core.IntegrationTests;

public sealed class BacklogsApplicationFactory : CustomWebApplicationFactory<Program>
{
    public BacklogsApplicationFactory()
        : base(dbContextType: typeof(BacklogsContext))
    {
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
}
