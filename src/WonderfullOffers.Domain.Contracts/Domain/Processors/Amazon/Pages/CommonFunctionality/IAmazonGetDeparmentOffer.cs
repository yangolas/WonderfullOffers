using HtmlAgilityPack;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;

public interface IAmazonGetDeparmentOffer : ITransientDomain
{
    Task<HtmlNode> GetClickDepartmentsAsync(
        HtmlNode htmlNode,
        IBrowserWeb browserWeb
    );
}