using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure.Modularization;

public interface IModule
{
    void Register(IServiceCollection services, IConfiguration configuration);
}
