using HtmlAgilityPack;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;

public interface IAmazonGetNextPage : ITransientDomain
{
    Task<HtmlNode> GetNextPageAsync(
        HtmlNode htmlNode,
        IBrowserWeb browserWeb
    );
}
