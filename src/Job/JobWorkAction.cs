using Quartz;
using System.Diagnostics;
using WonderfullOffers.Domain.Contracts.Domain.UpdateOffers;

namespace JobWork;

public class JobWorkAction : IJob
{
    private readonly IUpdateOfferService _updateOfferService;
    public JobWorkAction(IUpdateOfferService updateOfferService)
    {
        _updateOfferService = updateOfferService;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        Debug.WriteLine($"lanzando trigger");
        await _updateOfferService.RepositoriesToUpdateAsync();
    }
}