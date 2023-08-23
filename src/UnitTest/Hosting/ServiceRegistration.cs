using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WonderfullOffer.Api.Models.Settings.BrowserSettings;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Application.Contracts;
using WonderfullOffers.Domain.Contracts;
using WonderfullOffers.Infraestructure.Contracts;
using WonderfullOffers.Infraestructure.Entities;

namespace WonderfullOffers.Tests.UnitTest.Hosting;

public static class ServiceRegistration
{
    public static void AddServiceApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(WonderfullOffers.Application.ServiceRegistration).Assembly;
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

    public static void AddServiceDomain(this IServiceCollection services)
    {

        Assembly assembly = typeof(WonderfullOffers.Domain.ServiceRegistration).Assembly;
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

    public static void AddServiceInfraestructure(this IServiceCollection services)
    {
        Assembly assembly = typeof(Infraestructure.ServiceRegistration).Assembly;
        services.Scan(scan =>
            scan
                .FromAssemblies(assembly)
                .AddClasses(clases =>
                    clases.AssignableToAny(typeof(ITransientInfraestructure))
                )
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );

        services.AddDbContext<CompanyContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
    }

    public static void AddServicesSettings(this IServiceCollection services, IConfiguration configuration)
    {
        ServicesProcess(services, configuration);
        ServicesError(services, configuration);
        ServicesBrowser(services, configuration);
    }

    private static void ServicesBrowser(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BrowserSettings>(configuration.GetSection("Browser"));
    }

    private static void ServicesError(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ErrorSettings>(configuration.GetSection("Error"));
    }

    private static void ServicesProcess(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AmazonSettings>(configuration.GetSection("Amazon"));
        services.Configure<DouglasPageProcessSettings>(configuration.GetSection("DouglasPageProcess"));
        services.Configure<DruniPageProcessSettings>(configuration.GetSection("DruniPageProcess"));
        services.Configure<MaquillaliaPageProcessSettings>(configuration.GetSection("MaquillaliaPageProcess"));
        services.Configure<PrimorPageProcessSettings>(configuration.GetSection("PrimorPageProcess"));
        services.Configure<SheinPageProcessSettings>(configuration.GetSection("SheinPageProcess"));
        services.Configure<SpringfieldPageProcessSettings>(configuration.GetSection("SpringfieldPageProcess"));
        services.Configure<StradivariusPageProcessSettings>(configuration.GetSection("StradivariusPageProcess"));
        services.Configure<ZalandoPageProcessSettings>(configuration.GetSection("ZalandoPageProcess"));
        services.Configure<WomensecretPageProcessSettings>(configuration.GetSection("WomenSecretPageProcess"));
    }
}
