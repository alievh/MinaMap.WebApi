using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MapDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("PostgreSqlServer"), topologyOptions => topologyOptions.UseNetTopologySuite()));

        return services;
    }
}
