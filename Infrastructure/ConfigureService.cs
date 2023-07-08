using Application.Abstracts.Common;
using Infrastructure.Concretes.Common;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMapDbContext>(provider => provider.GetRequiredService<MapDbContext>());
        services.AddDbContext<MapDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("PostgreSqlServer"), topologyOptions => topologyOptions.UseNetTopologySuite()));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
