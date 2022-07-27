using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure.Validation;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic));

        return services;
    }
}
