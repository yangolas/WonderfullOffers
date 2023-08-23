using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Contracts.Domain.Processors.FindProcessBase;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.CommonFuntionalities;

public class AmazonHideCookies : IAmazonHideCookies
{
    private readonly IFindProcess _findProcess;
    private readonly AmazonSettings _amazonSettings;
    public AmazonHideCookies(
        IFindProcess findProcess,
        IOptions<AmazonSettings> optionsAmazon)
    {
        _findProcess = findProcess;
        _amazonSettings = optionsAmazon.Value;
    }

    public async Task<HtmlNode> HideCookiesAsync(
        HtmlNode htmlNode,
        IBrowserWeb browserWeb)
    {
        string? nodeCookies = await _findProcess.FindPathAsync(
            htmlNode,
            _amazonSettings.Cookies.NodeNameCookies
        );

        if (nodeCookies != null)
        {
            return await browserWeb.ClickRedirectAsync(nodeCookies);
        }

        return htmlNode;
    }
}
