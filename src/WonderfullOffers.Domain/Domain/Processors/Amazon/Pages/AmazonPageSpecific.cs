using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Domain.CustomException;
using WonderfullOffers.Domain.Models.Domain.Models.Contracts;
using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages;

public class AmazonPageSpecific : IAmazonPageFinal
{
    private readonly IExtractorAmazonProcess _extractorAmazon;
    private readonly IFindProcessAmazon _findProcessAmazon;
    private readonly AmazonSettings _amazonSettings;
    private readonly IOptions<ErrorSettings> _errorSettings;
    private readonly IAmazonHideCookies _amazonHideCookies;

    private IBrowserWeb? _browserWeb;
    private Uri? _currentUriPage;


    public AmazonPageSpecific(
        IAmazonHideCookies amazonHideCookies,
        IOptions<ErrorSettings> optionError,
        IOptions<AmazonSettings> optionAmazon,
        IFindProcessAmazon findProcessAmazon,
        IExtractorAmazonProcess extractorAmazon)
    {
        _amazonHideCookies = amazonHideCookies;
        _errorSettings = optionError;
        _findProcessAmazon = findProcessAmazon;
        _amazonSettings = optionAmazon.Value;
        _extractorAmazon = extractorAmazon;
    }

    private void CheckNullabiltySpecificResource()
    {
        if (
            _browserWeb == null
            || _currentUriPage == null)
        {
            var messageError = string.Format(
                _errorSettings.Value.NoSetResourcesPages,
                StackTree.GetPathError(new StackTrace(true))
            );

            throw new NullReferenceException(messageError);
        }
    }

    private async Task<IAmazonOffer> GetOffersAsync(HtmlNode pageSpecific, Uri img)
    {
        IAmazonOffer amazonOffer = new AmazonOffer();

        amazonOffer.Uri = await _browserWeb!.GetCurrentUrlAsync();

        HtmlNode nodeTitle = await _findProcessAmazon.FindNodeAsync(
            pageSpecific,
            _amazonSettings.PageSpecific.NodeNameTitle
        );

        string title = await _extractorAmazon.ExtractTextAsync(
            nodeTitle,
            _amazonSettings.PageSpecific.NodeNameTitle
        );

        amazonOffer.Title = title.Trim().ToLower();

        amazonOffer.Img = img;

        HtmlNode nodePriceWithinDisccount = await _findProcessAmazon.FindNodeAsync(
            pageSpecific,
            _amazonSettings.PageSpecific.NodeNamePriceWithinDiscount
        );

        decimal priceWhitinDisccount = await _extractorAmazon.ExtractPriceAsync(
                nodePriceWithinDisccount,
                _amazonSettings.PageSpecific.NodeNamePriceWithinDiscount
            );

        amazonOffer.PriceWithinDisccount = priceWhitinDisccount;

        try
        {
            HtmlNode nodeDisccount = await _findProcessAmazon.FindNodeAsync(
                pageSpecific,
                _amazonSettings.PageSpecific.NodeNameDiscount
            );

            int disccount = await _extractorAmazon.ExtractDisccountAsync(
                nodeDisccount,
                _amazonSettings.PageSpecific.NodeNameDiscount
            );

            amazonOffer.Disccount = disccount;
        }
        //It is a offer but there isnt the display
        catch (Exception) { }

        try
        {
            //There are pricewhitoutdisccount
            decimal? priceWithoutDiscount = await _extractorAmazon.ExtractAmazonPriceWithoutDiscountAsync(
                pageSpecific,
                _amazonSettings.PageSpecific.NodeNameParentPriceWithoutDiscount,
                _amazonSettings.PageSpecific.NodeNamePriceWithoutDiscount
            );

            amazonOffer.PriceWithoutDisccount = priceWithoutDiscount;
        }
        //There arent pricewhitoutdisccount
        catch (Exception) { }

        try
        {

            int? coupon = await _extractorAmazon.ExtractAmazonCouponAsync(
                pageSpecific,
                _amazonSettings.PageSpecific.NodeNameCoupon
            );
            amazonOffer.Coupon = coupon;
        }
        //Not all offers have coupons  ;(
        catch (Exception) { }

        amazonOffer.TimeSpan = DateTime.Now;
        return amazonOffer;
    }

    public async Task<IAmazonOffer> ProcessPageAsync(Uri img)
    {
        CheckNullabiltySpecificResource();

        HtmlNode pageSpecific = await _browserWeb!.NavigateAsync(_currentUriPage);

        await _amazonHideCookies.HideCookiesAsync(
            pageSpecific,
            _browserWeb
        );

        IAmazonOffer amazonOffer;

        try
        {
            amazonOffer = await GetOffersAsync(pageSpecific, img);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return amazonOffer;
    }

    public void SetSpecificResource(IBrowserWeb browserWeb)
    {
        _browserWeb = browserWeb;
    }

    public void SetUri(Uri uri)
    {
        _currentUriPage = uri;
    }

    public void Close()
    {
        _browserWeb!.Close();
        Thread.Sleep(400);
    }
}