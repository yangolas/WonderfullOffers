using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.CommonFuntionalities;

public class AmazonGetNextPage : IAmazonGetNextPage
{
    private readonly AmazonSettings _amazonSettings;
    private readonly IFindProcessAmazon _findProcessAmazon;
    public AmazonGetNextPage(
        IOptions<AmazonSettings> amazonSettings,
        IFindProcessAmazon findProcessAmazon)
    {
        _amazonSettings = amazonSettings.Value;
        _findProcessAmazon = findProcessAmazon;
    }

    public async Task<HtmlNode> GetNextPageAsync(
        HtmlNode htmlNode,
        IBrowserWeb browserWeb)
    {
        string? pathNextPagination = await _findProcessAmazon.FindAvailablePaginationPathNextAsync(
        htmlNode,
            $"{_amazonSettings.Pagination.NodeNextPageInCatalogParent}/{_amazonSettings.Pagination.NodeNextPageInCatalog}",
            _amazonSettings.Pagination.NodeFinnishPageInCatalog
        );

        if (!pathNextPagination.IsNullOrEmpty())
        {
            htmlNode = await browserWeb.ClickRedirectAsync(
                pathNextPagination,
                _amazonSettings.Pagination.NodeIdOverButton);

            return htmlNode;
        }
        else
        {
            return null;
        }
    }
}
