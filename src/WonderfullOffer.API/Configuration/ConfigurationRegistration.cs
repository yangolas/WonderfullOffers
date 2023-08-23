using WonderfullOffer.Api.Models.Settings.BrowserSettings;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;

namespace WonderfullOffer.API.Configuration;

public static class ConfigurationRegistration
{
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
