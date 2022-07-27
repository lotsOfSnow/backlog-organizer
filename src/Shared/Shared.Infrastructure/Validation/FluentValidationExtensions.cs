using BacklogOrganizer.Shared.Core.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure.Validation;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(Assemblies.GetAllNonDynamic());

        return services;
    }
}
