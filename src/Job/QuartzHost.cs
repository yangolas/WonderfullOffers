using Microsoft.Extensions.Hosting;
using Quartz;

namespace JobWork;

public class QuartzHost : IHostedService
{
    private readonly IScheduler _scheduler;

    public QuartzHost(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _scheduler.Start(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _scheduler.Shutdown(cancellationToken);
    }
}