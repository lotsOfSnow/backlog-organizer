using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BacklogOrganizer.Shared.Infrastructure.Postgres;

public static class PostgresExtensions
{
    public static IServiceCollection AddPostgres<TContext>(this IServiceCollection services, IConfiguration configuration)
        where TContext : DbContext
    {
        services.Configure<PostgresOptions>(configuration.GetSection("postgres"));
        services.AddDbContext<TContext>((serviceProvider, optionsBuilder) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<PostgresOptions>>();
            optionsBuilder.UseNpgsql(options.Value.ConnectionString);
        });

        return services;
    }
}
