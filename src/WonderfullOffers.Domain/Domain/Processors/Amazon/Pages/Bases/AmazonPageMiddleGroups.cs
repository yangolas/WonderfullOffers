using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Domain.CustomException;
using System.Diagnostics;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonContract;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Models.Domain.Models.UriImg;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.Bases;

public abstract class AmazonPageMiddleGroups : IAmazonPageMiddle, IAmazonPageMultyTask, IAmazonCreateAndConfigurePageSon
{
    protected readonly IExtractorAmazonProcess _extractorAmazon;
    protected readonly IFindProcessAmazon _findProcessAmazon;
    protected readonly IOptions<AmazonSettings> _optionAmazon;
    protected readonly IOptions<ErrorSettings> _optionError;
    protected readonly IAmazonHideCookies _amazonHideCookies;
    protected readonly IAmazonGetNextPage _amazonGetNextPage;
    protected readonly ILogger _logger;

    protected IBrowserWeb? _browserWeb;
    protected List<HtmlNode>? _nodesPageMiddleGroup;
    private Uri? _currentUriPage;

    private bool? _paginationOn;
    protected int? _numberOfTasks;

    protected Queue<IAmazonPageMiddle> _amazonPagesMiddle = new();

    public AmazonPageMiddleGroups(
        IAmazonHideCookies amazonHideCookies,
        IAmazonGetNextPage amazonGetNextPage,
        IOptions<ErrorSettings> optionError,
        IOptions<AmazonSettings> optionAmazon,
        IFindProcessAmazon findProcessAmazon,
        IExtractorAmazonProcess extractorAmazon,
        ILogger logger)
    {
        _amazonHideCookies = amazonHideCookies;
        _amazonGetNextPage = amazonGetNextPage;
        _optionError = optionError;
        _optionAmazon = optionAmazon;
        _findProcessAmazon = findProcessAmazon;
        _extractorAmazon = extractorAmazon;
        _logger = logger;
    }

    public abstract Task CreateAndConfigurePageSonAsync();

    protected void CheckNullabiltySpecificResource()
    {
        if (
            _browserWeb == null
            || _paginationOn == null
            || _numberOfTasks == null
            || _amazonPagesMiddle == null)
        {
            var messageError = string.Format(
                _optionError.Value.NoSetResourcesPages,
                StackTree.GetPathError(new StackTrace(true))
            );

            throw new NullReferenceException(messageError);
        }
    }

    public virtual async Task SetNodesToProcessAsync(HtmlNode pageToProcess)
    {
        _nodesPageMiddleGroup = await _findProcessAmazon.FindNodesAsync(
            pageToProcess,
            _optionAmazon.Value.PageGroupProviderAndMultyAndTypeS.NodeNameRedirectOffer
        );
    }

    public async Task SetSpecificResourcesAsync(
        IBrowserWeb browserWeb,
        int numberOfTasks,
        bool paginationOn)
    {
        _browserWeb = await browserWeb
            .NewBrowserWithMyConfigAsync();

        _paginationOn = paginationOn;
        _numberOfTasks = numberOfTasks;
    }

    public void SetUri(Uri uri)
    {
        _currentUriPage = uri;
    }

    public async Task<List<LinkAndImg>> ProcessPageLinksAndImgsAsync()
    {
        CheckNullabiltySpecificResource();

        List<LinkAndImg> listLinkAndImg = new();

        HtmlNode pageGroupProvider = await _browserWeb!.NavigateAsync(_currentUriPage!);

        pageGroupProvider = await _amazonHideCookies.HideCookiesAsync(
            pageGroupProvider,
            _browserWeb
        );

        bool isAllowNextPagination = true;
        List<Task> processPageTasks = new();

        while (isAllowNextPagination)
        {
            var uriPath = string.Empty;
            try
            {
                await SetNodesToProcessAsync(pageGroupProvider!);

                List<Uri> urisList = new(await _extractorAmazon.ExtractUrisAsync(_nodesPageMiddleGroup!));
                Queue<Uri> uris = new(urisList);

                for (int i = 0; i < _numberOfTasks; i++)
                {
                    processPageTasks.Add(Task.Run(async () =>
                    {
                        IAmazonPageMiddle amazonPagesProvider;
                        Uri uri;

                        while (true)
                        {
                            lock (uris)
                            {
                                if (uris.Count == 0)
                                    break;

                                uri = uris.Dequeue();
                            }

                            lock (_amazonPagesMiddle!)
                            {
                                amazonPagesProvider = _amazonPagesMiddle.Dequeue();
                                amazonPagesProvider.SetUri(uri);
                            }

                            try
                            {
                                List<LinkAndImg> listLinksImg = await amazonPagesProvider.ProcessPageLinksAndImgsAsync();

                                lock (listLinkAndImg)
                                {
                                    listLinkAndImg.AddRange(listLinksImg);
                                }

                                lock (_amazonPagesMiddle)
                                {
                                    _amazonPagesMiddle.Enqueue(amazonPagesProvider);
                                }
                            }
                            catch (Exception)
                            {
                                lock (_amazonPagesMiddle)
                                {
                                    _amazonPagesMiddle.Enqueue(amazonPagesProvider);
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

                if (_paginationOn ??= false)
                {
                    pageGroupProvider = await _amazonGetNextPage.GetNextPageAsync(
                        pageGroupProvider!,
                        _browserWeb
                    );

                    isAllowNextPagination = pageGroupProvider != null;
                }
                else
                {
                    isAllowNextPagination = false;
                }
            }
            catch (Exception)
            {
                _logger.LogWarning(
                    _optionError.Value.ScratcherPageDifferentType,
                    StackTree.GetPathError(new StackTrace(true)),
                    uriPath
                );
            }
        }

        return listLinkAndImg;
    }

    public void Close()
    {
        _browserWeb!.Close();
        while (_amazonPagesMiddle!.Count != 0)
        {
            _amazonPagesMiddle.Dequeue().Close();
        }
    }
}
