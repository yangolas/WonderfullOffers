using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonContract;
using WonderfullOffers.Domain.Models.Domain.Models.Contracts;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;

//public interface IAmazonPageFinal : IAmazonSetUri, IDisposable
public interface IAmazonPageFinal : IAmazonSetUri
{
    Task<IAmazonOffer> ProcessPageAsync(Uri img);
    void SetSpecificResource(IBrowserWeb browserWeb);
    void Close();
}