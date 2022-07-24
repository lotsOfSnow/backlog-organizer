using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure.Modularization;

internal static class ModuleLoader
{
    public static IServiceCollection Load(IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        foreach (var module in GetAllModules(assemblies))
        {
            module.Register(services, configuration);
        }

        return services;
    }

    private static IEnumerable<IModule> GetAllModules(IEnumerable<Assembly> assemblies)
        => assemblies.SelectMany(x => x.GetTypes())
            .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();
}
