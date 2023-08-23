using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WonderfullOffers.Infraestructure.Contracts;
using WonderfullOffers.Infraestructure.Entities;

namespace WonderfullOffers.Infraestructure;

public static class ServiceRegistration
{
    public static void AddServiceInfraestructure(
        this IServiceCollection services, 
        IConfiguration configuration  ) 
    {
        services.AddDbContext<CompanyContext>(options =>
                options.UseSqlServer(configuration["ConnectionString"])
            );

        Assembly assembly = typeof(ServiceRegistration).Assembly;
        services.Scan(scan =>
            scan
                .FromAssemblies(assembly)
                .AddClasses(clases =>
                    clases.AssignableToAny(typeof(ITransientInfraestructure))
                )
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );
    }
}