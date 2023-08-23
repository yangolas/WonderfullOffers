using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.BrowserSettings;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonContract;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;
using WonderfullOffers.Domain.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Domain.CustomException;
using WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.Bases;
using WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.CommonFuntionalities;
using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;
using WonderfullOffers.Domain.Models.Domain.Models.UriImg;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages;

public class AmazonPageMerge : IAmazonPageMain, IAmazonPageMultyTask
{
    private readonly IAmazonGetNextPage _amazonGetNextPage;
    private readonly IAmazonHideCookies _amazonHideCookies;
    private readonly IAmazonGetDeparmentOffer _amazonGetDeparmentOffer;
    private readonly IExtractorAmazonProcess _extractorAmazon;
    private readonly IFindProcessAmazon _findProcessAmazon;
    private readonly IOptions<AmazonSettings> _optionAmazon;
    private readonly IOptions<ErrorSettings> _optionError;
    private readonly ILogger _logger;
    private readonly IBrowserWeb _browserWeb;

    private Uri? _currentUriPage;

    private IAmazonPageMiddle? _pageGroupProvider;
    private IAmazonPageMiddle? _pageGroupTypeS;
    private IAmazonPageMiddle? _pageMultyOffer;
    private bool _flagActivePageTypeS = false;

    public AmazonPageMerge(
        IAmazonHideCookies amazonHideCookies,
        IAmazonGetNextPage amazonGetNextPage,
        IAmazonGetDeparmentOffer amazonGetDeparmentOffer,
        IOptions<ErrorSettings> optionError,
        IOptions<AmazonSettings> optionAmazon,
        IFindProcessAmazon findProcessAmazon,
        IExtractorAmazonProcess extractorAmazon,
        IOptions<BrowserSettings> optionBrowser,
        IHtmlAgilityPackageProcess htmlAgilityPackageProcess,
        ILogger logger)
    {
        _amazonHideCookies = amazonHideCookies;
        _amazonGetNextPage = amazonGetNextPage;
        _amazonGetDeparmentOffer = amazonGetDeparmentOffer;
        _optionError = optionError;
        _optionAmazon = optionAmazon;
        _findProcessAmazon = findProcessAmazon;
        _extractorAmazon = extractorAmazon;
        _logger = logger;

        _browserWeb = new BrowserWeb(
            optionBrowser,
            htmlAgilityPackageProcess,
            optionError
        );
    }


    public async Task CreateAndConfigurePageSonAsync()
    {
        AmazonPageGroupProvider pageGroupProvider = new(
            _amazonHideCookies,
            _amazonGetNextPage,
            _optionError,
            _optionAmazon,
            _findProcessAmazon,
            _extractorAmazon,
            _logger
        );

        AmazonPageGroupTypeS pageGroupTypeS = new (
            _amazonHideCookies,
            _amazonGetNextPage,
            _optionError,
            _optionAmazon,
            _findProcessAmazon,
            _extractorAmazon,
            _logger
        );

        _pageMultyOffer = new AmazonPageMulty(
            _amazonHideCookies,
            _amazonGetNextPage,
            _optionError,
            _optionAmazon,
            _findProcessAmazon,
            _extractorAmazon,
            _logger
        );

        if (_currentUriPage!.OriginalString.Contains("outlet")) 
        {
            await SetResourcesAndConfigurationPageGroup(1, pageGroupProvider);
            _pageGroupProvider = pageGroupProvider;

            await SetResourcesAndConfigurationPageGroup(6, pageGroupTypeS);
            _pageGroupTypeS = pageGroupTypeS;
        }
        else if (_currentUriPage!.OriginalString.Contains("goldbox")) 
        {
            await SetResourcesAndConfigurationPageGroup(6, pageGroupProvider);
            _pageGroupProvider = pageGroupProvider;

            await SetResourcesAndConfigurationPageGroup(1, pageGroupTypeS);
            _pageGroupTypeS = pageGroupTypeS;
        }
        else 
        {
            await SetResourcesAndConfigurationPageGroup(4, pageGroupProvider);
            _pageGroupProvider = pageGroupProvider;

            await SetResourcesAndConfigurationPageGroup(3, pageGroupTypeS);
            _pageGroupTypeS = pageGroupTypeS;
        }

        await SetPageMiddleResources(1, _pageMultyOffer);

        CheckNullabiltySpecificResource();
    }

    private void CheckNullabiltySpecificResource()
    {
        if (
            _pageMultyOffer == null
            || _pageGroupProvider == null)
        {
            string messageError = string.Format(
                _optionError.Value.NoSetResourcesPages,
                StackTree.GetPathError(new StackTrace(true))
            );

            throw new Exception(messageError);
        }
    }

    private async Task<bool> CheckIfPageGroupTypeSIsActivated(HtmlNode PageMerge)
    {
        List<HtmlNode> nodesMerge = await _findProcessAmazon.FindNodesAsync(
            PageMerge,
            _optionAmazon.Value.PageGroupProviderAndMultyAndTypeS.NodeNameRedirectOffer
        );

        return !(await _findProcessAmazon.GetNodesGroupTypeS(nodesMerge)).IsNullOrEmpty();
    }

    public void SetUri(Uri uri)
    {
        _currentUriPage = uri;
    }


    private async Task SetResourcesAndConfigurationPageGroup(
        int numberOfTasks, 
        AmazonPageMiddleGroups pageGroup)
    {
        await pageGroup.SetSpecificResourcesAsync(
            _browserWeb!,
            numberOfTasks,
            false
        );

        await pageGroup.CreateAndConfigurePageSonAsync();
    }

    private async Task SetPageMiddleResources(
        int numberOfTask, 
        IAmazonPageMiddle pageMiddle) 
    {
        await pageMiddle.SetSpecificResourcesAsync(
            _browserWeb!,
            numberOfTask,
            false
        );
    }

    private async Task<List<LinkAndImg>> ProcessPageLinksAndImgsAsync(
        IAmazonPageMiddle pageMiddle,
        Uri uri
    )
    {
        pageMiddle.SetUri(uri);

        return await pageMiddle.ProcessPageLinksAndImgsAsync();
    }

    public async Task<List<LinkAndImg>> ProcessPageLinksAndImgsAsync()
    {
        CheckNullabiltySpecificResource();

        List<LinkAndImg> _listLinkAndImg = new();
        List<AmazonOffer> offers = new();

        HtmlNode pageMerge = await _browserWeb!.NavigateAsync(_currentUriPage!);

        pageMerge = await _amazonHideCookies.HideCookiesAsync(
            pageMerge,
            _browserWeb
        );

        pageMerge = await _amazonGetDeparmentOffer.GetClickDepartmentsAsync(
            pageMerge,
            _browserWeb
        );

        bool isAllowNextPagination = true;

        while (isAllowNextPagination)
        {
            try
            {
                Task<List<LinkAndImg>> taskGroupPageTypeS = null;
                Uri uri = await _browserWeb.GetCurrentUrlAsync();

                if (_flagActivePageTypeS)
                {
                    taskGroupPageTypeS = Task.Run(() =>
                        ProcessPageLinksAndImgsAsync(_pageGroupTypeS!, uri)
                    );
                }
                else
                {
                    _flagActivePageTypeS = await CheckIfPageGroupTypeSIsActivated(pageMerge!);
                    if (_flagActivePageTypeS)
                        taskGroupPageTypeS = ProcessPageLinksAndImgsAsync(_pageGroupTypeS!, uri);
                }

                Task<List<LinkAndImg>> taskGroupProvider = Task.Run(() =>
                    ProcessPageLinksAndImgsAsync(_pageGroupProvider!, uri)
                );

                Task<List<LinkAndImg>> taskMultyOffer = Task.Run(() =>
                    ProcessPageLinksAndImgsAsync(_pageMultyOffer!, uri)
                );

                if (taskGroupPageTypeS != null)
                {
                    await Task.WhenAll(taskMultyOffer, taskGroupProvider, taskGroupPageTypeS);
                    _listLinkAndImg.AddRange(taskGroupPageTypeS.Result);
                }
                else
                {
                    await Task.WhenAll(taskMultyOffer, taskGroupProvider);
                }

                _listLinkAndImg.AddRange(taskMultyOffer.Result);
                _listLinkAndImg.AddRange(taskGroupProvider.Result);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            pageMerge = await _amazonGetNextPage.GetNextPageAsync(
                pageMerge!,
                _browserWeb
            );

            isAllowNextPagination = pageMerge != null;
        }
        return _listLinkAndImg;
    }

    public void Close()
    {
        _browserWeb!.Close();
        _pageGroupProvider?.Close();
        _pageGroupTypeS?.Close();
        _pageMultyOffer?.Close();
        Thread.Sleep(5000);
    }
}