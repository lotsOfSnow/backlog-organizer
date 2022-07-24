using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BacklogOrganizer.Shared.Infrastructure.Modularization;

public interface IModule
{
    public void Register(IServiceCollection services, IConfiguration configuration);
}
