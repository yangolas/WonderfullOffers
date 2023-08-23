using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WonderfullOffers.Application.Contracts;

namespace WonderfullOffers.Application;

public static class ServiceRegistration
{
    public static void AddServiceApplication(this IServiceCollection services) 
    {
        Assembly assembly = typeof(ServiceRegistration).Assembly;
        services.Scan(scan =>
            scan
                .FromAssemblies(assembly)
                .AddClasses(clases => 
                    clases.AssignableToAny(typeof(ITransientUseCase))
                )
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );

    }
}
