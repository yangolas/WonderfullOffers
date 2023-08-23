using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.BrowserSettings;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;
using WonderfullOffers.Domain.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Domain.CustomException;
using WonderfullOffers.Domain.Domain.Processors.Amazon.Pages;
using WonderfullOffers.Domain.Models.Domain.Models.Contracts;
using WonderfullOffers.Domain.Models.Domain.Models.UriImg;
using WonderfullOffers.Infraestructure.Contracts.Repository;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon;

public class AmazonProcess : IAmazonProcess
{
    private readonly IExtractorAmazonProcess _extractorAmazon;
    private readonly IFindProcessAmazon _findProcessAmazon;
    private readonly ILogger<AmazonProcess> _logger;
    private readonly IOptions<BrowserSettings> _optionBrowser;
    private readonly IHtmlAgilityPackageProcess _htmlAgilityPackageProcess;
    private readonly IOptions<ErrorSettings> _optionError;
    private readonly IOptions<AmazonSettings> _optionAmazon;
    private readonly IAmazonHideCookies _amazonHideCookies;
    private readonly IAmazonGetNextPage _amazonGetNextPage;
    private readonly IAmazonGetDeparmentOffer _amazonGetDeparmentOffer;


    public AmazonProcess(
        IOptions<AmazonSettings> optionsAmazonSettings,
        IExtractorAmazonProcess extractorAmazonProcess,
        IFindProcessAmazon findProcess,
        IOptions<BrowserSettings> optionsBrowserSettings,
        ILogger<AmazonProcess> logger,
        IOptions<ErrorSettings> optionsErrorSettings,
        IHtmlAgilityPackageProcess htmlAgilityPackageProcess,
        IAmazonHideCookies amazonHideCookies,
        IAmazonGetNextPage amazonGetNextPage,
        IAmazonGetDeparmentOffer amazonGetDeparmentOffer)
    {
        _extractorAmazon = extractorAmazonProcess;
        _findProcessAmazon = findProcess;

        _optionBrowser = optionsBrowserSettings;
        _htmlAgilityPackageProcess = htmlAgilityPackageProcess;
        _optionError = optionsErrorSettings;
        _optionAmazon = optionsAmazonSettings;

        _logger = logger;

        _amazonHideCookies = amazonHideCookies;
        _amazonGetNextPage = amazonGetNextPage;
        _amazonGetDeparmentOffer = amazonGetDeparmentOffer;
    }
    private async Task<Queue<IAmazonPageFinal>> CreateProcesorsExtractionOffersAsync(int _numberOfTasks)
    {
        List<Task> processPageTasks = new();
        Queue<IAmazonPageFinal> amazonPageFinals = new();

        for (int indexTask = 0; indexTask < _numberOfTasks; indexTask++)
        {
            processPageTasks.Add(
                Task.Run(() =>
                {
                    IAmazonPageFinal amazonPageSpecific = new AmazonPageSpecific(
                        _amazonHideCookies,
                        _optionError,
                        _optionAmazon,
                        _findProcessAmazon,
                        _extractorAmazon
                    );

                    IBrowserWeb newBrowser = new BrowserWeb(
                        _optionBrowser,
                        _htmlAgilityPackageProcess,
                        _optionError
                    );

                    amazonPageSpecific.SetSpecificResource(newBrowser);

                    amazonPageFinals!.Enqueue(amazonPageSpecific);
                })
            );
        }

        await Task.WhenAll(processPageTasks);
        return amazonPageFinals;
    }

    public bool IsThisProcess(Type webType)
    {
        return typeof(IAmazonRepository).Equals(webType);
    }

    private async Task<List<IAmazonOffer>> ProcessOffersAsync(
        Queue<LinkAndImg> queueLinkAndImg,
        int numberOfTasks)
    {
        Queue<IAmazonPageFinal> pagesFinal = await CreateProcesorsExtractionOffersAsync(numberOfTasks);
        List<IAmazonOffer> offers = new();
        List<Task> processPageTasks = new();

        for (int i = 0; i < numberOfTasks; i++)
        {
            processPageTasks.Add(Task.Run(async () =>
            {
                IAmazonPageFinal amazonPageSpecific;
                LinkAndImg linkAndImg;
                Uri uri;
                Uri img;

                while (true)
                {
                    lock (queueLinkAndImg)
                    {
                        if (queueLinkAndImg.Count == 0)
                            break;

                        linkAndImg = queueLinkAndImg.Dequeue();
                        uri = linkAndImg.Link;
                        img = linkAndImg.Img;
                    }

                    lock (pagesFinal!)
                    {
                        amazonPageSpecific = pagesFinal.Dequeue();
                        amazonPageSpecific.SetUri(uri);
                    }

                    try
                    {
                        IAmazonOffer offer = await amazonPageSpecific.ProcessPageAsync(img);

                        lock (offers)
                        {
                            offers.Add(offer);
                        }

                        lock (pagesFinal)
                        {
                            pagesFinal.Enqueue(amazonPageSpecific);
                        }
                    }
                    catch (Exception)
                    {
                        lock (pagesFinal)
                        {
                            pagesFinal.Enqueue(amazonPageSpecific);
                        }
                        var messageError = string.Format(
                            _optionError.Value.ScratcherPageDifferentType,
                            StackTree.GetPathError(new StackTrace(true)),
                            uri.OriginalString);

                        _logger.LogWarning(messageError);
                    }
                }
            }));
        }

        await Task.WhenAll(processPageTasks);

        while (pagesFinal.Count != 0)
        {
            pagesFinal.Dequeue().Close();
        }

        return offers;
    }

    public async Task<List<IOffer>> ProcessOffersCompanyAsync()
    {
        List<IAmazonOffer> offers = new();
        Queue<LinkAndImg> queueLinkAndImgs = new();
        try
        {
            List<Uri> urisCatalog = _optionAmazon.Value.AmazonUrls
                .Select(uri => new Uri(uri)).ToList();

            foreach (var uriCatalog in urisCatalog)
            {
                IAmazonPageMain pageMain = new AmazonPageMerge(
                    _amazonHideCookies,
                    _amazonGetNextPage,
                    _amazonGetDeparmentOffer,
                    _optionError,
                    _optionAmazon,
                    _findProcessAmazon,
                    _extractorAmazon,
                    _optionBrowser,
                    _htmlAgilityPackageProcess,
                    _logger
                );

                pageMain.SetUri(uriCatalog);

                await pageMain.CreateAndConfigurePageSonAsync();

                List<LinkAndImg> listLinkAndImgsPageMain = await pageMain.ProcessPageLinksAndImgsAsync();
                listLinkAndImgsPageMain.ForEach(linkAndImg => queueLinkAndImgs.Enqueue(linkAndImg));

                pageMain.Close();

                offers.AddRange(await ProcessOffersAsync(queueLinkAndImgs, _optionAmazon.Value.NumberOfTasksRunning));
            }
        }
        catch (Exception ex)
        {
            string messageException = string.Format(
                _optionError.Value.PageWebNotFound,
                ex.Message
            );

            _logger.LogWarning(messageException);
        }

        return offers.Cast<IOffer>().ToList();
    }
}