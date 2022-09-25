using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure.Options;

public static class OptionsExtensions
{
    public static T ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
        where T : class, new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        services.Configure<T>(_ => _ = options);
        return options;
    }
}
