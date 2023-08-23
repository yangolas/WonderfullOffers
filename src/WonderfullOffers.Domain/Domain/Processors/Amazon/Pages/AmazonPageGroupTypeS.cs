using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.Bases;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages;

public class AmazonPageGroupTypeS : AmazonPageMiddleGroups
{
    public AmazonPageGroupTypeS(
        IAmazonHideCookies amazonHideCookies,
        IAmazonGetNextPage amazonGetNextPage,
        IOptions<ErrorSettings> optionError,
        IOptions<AmazonSettings> optionAmazon,
        IFindProcessAmazon findProcessAmazon,
        IExtractorAmazonProcess extractorAmazon,
        ILogger logger) :
        base(
            amazonHideCookies,
            amazonGetNextPage,
            optionError,
            optionAmazon,
            findProcessAmazon,
            extractorAmazon,
            logger
        ){}

    public override async Task CreateAndConfigurePageSonAsync()
    {
        List<Task> processPageTasks = new();

        for (int indexTask = 0; indexTask < _numberOfTasks; indexTask++)
        {
            processPageTasks.Add(
                Task.Run(async () =>
                {
                    IAmazonPageMiddle amazonPageMiddle = new AmazonPageTypologyS(
                        _amazonHideCookies,
                        _amazonGetNextPage,
                        _optionError,
                        _optionAmazon,
                        _findProcessAmazon,
                        _extractorAmazon,
                        _logger
                    );

                    await amazonPageMiddle.SetSpecificResourcesAsync(
                        _browserWeb!,
                        1,
                        true
                    );

                    _amazonPagesMiddle!.Enqueue(amazonPageMiddle);
                })
            );
        }

        await Task.WhenAll(processPageTasks);
        CheckNullabiltySpecificResource();
    }

    public override async Task SetNodesToProcessAsync(HtmlNode pageToProcess)
    {
        List<HtmlNode> nodesOffer = await _findProcessAmazon.FindNodesAsync(
            pageToProcess,
            _optionAmazon.Value.PageGroupProviderAndMultyAndTypeS.NodeNameRedirectOffer
        );

        _nodesPageMiddleGroup = await _findProcessAmazon
            .GetNodesGroupTypeS(nodesOffer);
    }
}