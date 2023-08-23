using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;
using WonderfullOffers.Domain.Domain.Processors.FindProcessBase;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon;

public class FindProcessAmazon : FindProcess, IFindProcessAmazon
{
    private readonly IExtractorAmazonProcess _extractorAmazon;
    public FindProcessAmazon(
        IHtmlAgilityPackageProcess htmlAgilityPackageProcess,
        IOptions<ErrorSettings> optionsErrorSettings,
        ILogger<FindProcess> logger,
        IExtractorAmazonProcess extractorAmazonProcess)
    : base(
        htmlAgilityPackageProcess,
        optionsErrorSettings,
        logger
    )
    {
        _extractorAmazon = extractorAmazonProcess;
    }

    public async Task<HtmlNode> GetNodeMultyAsync(HtmlNode nodeOffer)
    {
        Uri uri = await _extractorAmazon.ExtractUriAsync(nodeOffer);
        if (!uri.OriginalString.Contains("/deal/") && !uri.OriginalString.Contains("/s/"))
            return nodeOffer;

        return null;
    }

    public async Task<List<HtmlNode>> GetNodesMultyAsync(List<HtmlNode> nodeOffers)
    {
        List<HtmlNode> nodesMultyPage = new();

        foreach (var nodeOffer in nodeOffers)
        {
            HtmlNode nodeMultyPage = await GetNodeMultyAsync(nodeOffer);

            if (nodeMultyPage != null)
                nodesMultyPage.Add(nodeMultyPage);
        }

        return nodesMultyPage;
    }

    public async Task<HtmlNode> GetNodeGroupProviderAsync(HtmlNode nodeOffer)
    {
        Uri uri = await _extractorAmazon.ExtractUriAsync(nodeOffer);
        if (uri.OriginalString.Contains("/deal/"))
            return nodeOffer;

        return null;
    }

    public async Task<List<HtmlNode>> GetNodesGroupProviderAsync(List<HtmlNode> nodeOffers)
    {
        List<HtmlNode> nodesGroupProvidersPage = new();
        foreach (var nodeOffer in nodeOffers)
        {
            HtmlNode nodeGroupProvidersPage = await GetNodeGroupProviderAsync(nodeOffer);

            if (nodeGroupProvidersPage != null)
                nodesGroupProvidersPage.Add(nodeGroupProvidersPage);
        }
        return nodesGroupProvidersPage;
    }

    public async Task<HtmlNode> GetNodeGroupTypeS(HtmlNode nodeOffer)
    {
        Uri uri = await _extractorAmazon.ExtractUriAsync(nodeOffer);
        if (uri.OriginalString.Contains("/s/"))
            return nodeOffer;

        return null;
    }

    public async Task<List<HtmlNode>> GetNodesGroupTypeS(List<HtmlNode> nodeOffers)
    {
        List<HtmlNode> nodesGroupTypeS = new();
        foreach (var nodeOffer in nodeOffers)
        {
            HtmlNode nodeGroupTypeS = await GetNodeGroupTypeS(nodeOffer);

            if (nodeGroupTypeS != null)
                nodesGroupTypeS.Add(nodeGroupTypeS);
        }
        return nodesGroupTypeS;
    }
}