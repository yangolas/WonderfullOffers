using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace WonderfullOffers.Tests.UnitTest.Hosting;

public class Host
{
    private HostApplicationBuilder _builder { get; }

    public Host()
    {
        _builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder();
        ConfigureService();

    }
    private void ConfigureService() 
    {
        var testDirectory = Directory.GetCurrentDirectory();

        var appsettingsPath = Path.Combine(
            testDirectory,
            "..", 
            "..", 
            "..", 
            "..",
            "WonderfullOffer.API", 
            "Appsettings.json"
        );

        _builder.Configuration
            .SetBasePath(testDirectory)
            .AddJsonFile(appsettingsPath, optional: true, reloadOnChange: true);

        _builder.Services.AddServiceApplication();
        _builder.Services.AddServiceDomain();
        _builder.Services.AddServiceInfraestructure();
        _builder.Services.AddServicesSettings(_builder.Configuration);

        _builder.Services.AddSerilog((ctx, lc) =>
            lc.MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
        );

        _builder.Build();
    }

    public void ReplaceServiceForMock<TMock>(Mock<TMock> mock) where TMock : class
    {
        _builder.Services.AddTransient(_=> mock.Object);
    }

    public TService GetService<TService>()
    {
        var serviceProvider = _builder.Services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<TService>();
    }
}
