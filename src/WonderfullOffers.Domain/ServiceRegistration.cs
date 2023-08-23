using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WonderfullOffers.Domain.Contracts;

namespace WonderfullOffers.Domain;

public static class ServiceRegistration
{
    public static void AddServiceDomain(this IServiceCollection services)
    {
        Assembly assembly = typeof(ServiceRegistration).Assembly;
        services.Scan(scan =>
            scan
                .FromAssemblies(assembly)
                .AddClasses(clases =>
                    clases.AssignableToAny(typeof(ITransientDomain))
                )
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );
    }
}
