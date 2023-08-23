using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.Bases;
using WonderfullOffers.Domain.Models.Domain.Models.UriImg;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages;

public class AmazonPageMulty : AmazonPageMiddleLastStep
{
    private readonly IOptions<AmazonSettings> _optionAmazon;
    public AmazonPageMulty(
        IAmazonHideCookies amazonHideCookies,
        IAmazonGetNextPage amazonGetNextPage,
        IOptions<ErrorSettings> optionError,
        IOptions<AmazonSettings> optionAmazon,
        IFindProcessAmazon findProcessAmazon,
        IExtractorAmazonProcess extractorAmazon,
        ILogger logger)
        : base(
            amazonHideCookies,
            amazonGetNextPage,
            optionAmazon,
            optionError,
            findProcessAmazon,
            extractorAmazon,
            logger)
    {
        _optionAmazon = optionAmazon;
    }

    protected override async Task AddLinkAndImg()
    {
        CheckNullabiltySpecificResource();

        foreach (var node in _nodesPageMiddle!)
        {
            LinkAndImg linkAndImg = new();

            linkAndImg.Img = await _extractorAmazon.ExtractImgAsync(node);
            linkAndImg.Link = await _extractorAmazon.ExtractUriAsync(node);

            _listLinkAndImg.Add(linkAndImg);
        }
    }

    public override async Task SetSpecificResourcesAsync(
        IBrowserWeb browserWeb,
        int numberOfTasks,
        bool paginationOn)
    {
        await base.SetSpecificResourcesAsync(
            browserWeb,
            numberOfTasks,
            paginationOn
        );

        _nodeNameRedirectLink = _optionAmazon.Value
            .PageGroupProviderAndMultyAndTypeS.NodeNameRedirectOffer;
    }

    protected async override Task SetNodesToProcessAsync(HtmlNode pageToProcess)
    {
        await base.SetNodesToProcessAsync(pageToProcess);

        CheckNullabiltySpecificResource();

        _nodesPageMiddle = await _findProcessAmazon.GetNodesMultyAsync(_nodesPageMiddle!);
    }
}