using BacklogOrganizer.Shared.Core.Mediator.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Core.Mediator;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
