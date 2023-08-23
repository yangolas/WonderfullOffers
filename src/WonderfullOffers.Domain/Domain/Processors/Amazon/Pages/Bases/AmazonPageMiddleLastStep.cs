using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Domain.CustomException;
using WonderfullOffers.Domain.Models.Domain.Models.UriImg;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.Bases;

public abstract class AmazonPageMiddleLastStep : IAmazonPageMiddle
{
    protected List<HtmlNode>? _nodesPageMiddle;

    protected readonly IExtractorAmazonProcess _extractorAmazon;
    protected readonly IFindProcessAmazon _findProcessAmazon;
    private readonly IOptions<AmazonSettings> _optionAmazon;
    private readonly IOptions<ErrorSettings> _optionError;
    private readonly IAmazonHideCookies _amazonHideCookies;
    private readonly IAmazonGetNextPage _amazonGetNextPage;
    private readonly ILogger _logger;

    private IBrowserWeb? _browserWeb;
    private bool? _paginationOn;
    private int? _numberOfTasks;
    private Uri? _currentUriPage;

    protected string? _nodeNameRedirectLink;
    protected List<LinkAndImg> _listLinkAndImg;

    public AmazonPageMiddleLastStep(
        IAmazonHideCookies amazonHideCookies,
        IAmazonGetNextPage amazonGetNextPage,
        IOptions<AmazonSettings> optionAmazon,
        IOptions<ErrorSettings> optionError,
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

    protected void CheckNullabiltySpecificResource()
    {
        if (
            _browserWeb == null
            || _paginationOn == null
            || _numberOfTasks == null)
        {
            string messageError = string.Format(
                _optionError.Value.NoSetResourcesPages,
                StackTree.GetPathError(new StackTrace(true))
            );

            throw new NullReferenceException(messageError);
        }
    }

    private void CheckNullabiltySonResource()
    {
        if (
            _currentUriPage == null
            || _nodeNameRedirectLink == null)
        {
            string messageError = string.Format(
                _optionError.Value.NoSetResourcesPages,
                StackTree.GetPathError(new StackTrace(true))
            );

            throw new NullReferenceException(messageError);
        }
    }

    protected abstract Task AddLinkAndImg();

    protected async virtual Task SetNodesToProcessAsync(HtmlNode pageToProcess)
    {
        CheckNullabiltySpecificResource();

        _nodesPageMiddle = await _findProcessAmazon.FindNodesAsync(
            pageToProcess,
            _nodeNameRedirectLink!
        );
    }

    public virtual async Task SetSpecificResourcesAsync(
        IBrowserWeb browserWeb,
        int numberOfTasks,
        bool paginationOn)
    {
        _browserWeb = await browserWeb.NewBrowserWithMyConfigAsync();
        _numberOfTasks = numberOfTasks;
        _paginationOn = paginationOn;
    }

    public void SetUri(Uri uri)
    {
        _currentUriPage = uri;
    }

    public async Task<List<LinkAndImg>> ProcessPageLinksAndImgsAsync()
    {
        _listLinkAndImg = new();
        CheckNullabiltySpecificResource();
        CheckNullabiltySonResource();

        HtmlNode pageProvider = await _browserWeb!.NavigateAsync(_currentUriPage!);

        pageProvider = await _amazonHideCookies.HideCookiesAsync(
            pageProvider,
            _browserWeb
        );

        bool isAllowNextPagination = true;

        while (isAllowNextPagination)
        {
            try
            {
                await SetNodesToProcessAsync(pageProvider!);
                await AddLinkAndImg();
            }
            catch (Exception)
            {
                var messageError = string.Format(
                    _optionError.Value.ScratcherPageDifferentType,
                    StackTree.GetPathError(new StackTrace(true)),
                    $"Pagination Provider: { await _browserWeb.GetCurrentUrlAsync()}");

                _logger.LogWarning(messageError);
            }
            if (_paginationOn ??= false)
            {
                pageProvider = await _amazonGetNextPage.GetNextPageAsync(
                    pageProvider!,
                    _browserWeb
                );

                isAllowNextPagination = pageProvider != null;
            }
            else
            {
                isAllowNextPagination = false;
            }
        }

        return _listLinkAndImg;
    }

    public void Close()
    {
        _browserWeb!.Close();
    }
}
