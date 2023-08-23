using HtmlAgilityPack;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;

public interface IAmazonHideCookies : ITransientDomain
{
    Task<HtmlNode> HideCookiesAsync(
        HtmlNode htmlNode,
        IBrowserWeb browserWeb);
}
