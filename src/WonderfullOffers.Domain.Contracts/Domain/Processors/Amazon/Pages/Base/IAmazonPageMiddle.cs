using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonContract;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;

//public interface IAmazonPageMiddle : IAmazonProcessPageUris, IAmazonSetUri, IDisposable
public interface IAmazonPageMiddle : IAmazonProcessPageLinksAndImgs, IAmazonSetUri
{
    Task SetSpecificResourcesAsync(
        IBrowserWeb browserWeb,
        int numberOfTaskProcess,
        bool paginationOn);

    void Close();
}