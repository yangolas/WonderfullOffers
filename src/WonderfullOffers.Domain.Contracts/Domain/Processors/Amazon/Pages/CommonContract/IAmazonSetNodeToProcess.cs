using HtmlAgilityPack;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonContract;

public interface IAmazonSetNodeToProcess
{
    Task SetNodesToProcessAsync(HtmlNode pageToProcess);
}
