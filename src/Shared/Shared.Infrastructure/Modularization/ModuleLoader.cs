using System.Reflection;
using BacklogOrganizer.Shared.Core.Reflection;
using BacklogOrganizer.Shared.Core.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure.Modularization;

internal static class ModuleLoader
{
    private const string ModuleIdentifier = ".Module.";

    public static IServiceCollection Load(IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = LoadAssemblies(configuration);

        foreach (var module in GetAllModules(assemblies))
        {
            module.Register(services, configuration);
        }

        return services;
    }

    // Assemblies are only loaded on first use, so not loading them explicitly will make it ignore all the referenced modules.
    private static IEnumerable<Assembly> LoadAssemblies(IConfiguration configuration)
    {
        var alreadyLoadedAssemblies = Assemblies.GetAll().ToList();
        var loadedAssembliesLocations = alreadyLoadedAssemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();

        var notYetLoadedModulesDlls = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !loadedAssembliesLocations.Contains(x, StringComparer.OrdinalIgnoreCase))
            .Where(x => x.Contains(ModuleIdentifier)).ToList();

        foreach (var dllPath in notYetLoadedModulesDlls)
        {
            var loadedAssembly = AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(dllPath));
            alreadyLoadedAssemblies.Add(loadedAssembly);
        }

        return alreadyLoadedAssemblies;
    }


    private static IEnumerable<IModule> GetAllModules(IEnumerable<Assembly> assemblies)
        => assemblies.SelectMany(x => x.GetTypes())
            .Where(x => x.IsConcreteImplementationOf<IModule>())
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();
}
