using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;
using WonderfullOffers.Domain.Domain.Processors.ExtractorProcessBase;
using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon;

public class ExtractorAmazonProcess : ExtractorProcess, IExtractorAmazonProcess
{
    private readonly IHtmlAgilityPackageProcess _htmlAgilityPackageProcess;
    private readonly ErrorSettings _errorSettings;
    private readonly ILogger<ExtractorAmazonProcess> _logger;
    public ExtractorAmazonProcess(
        IHtmlAgilityPackageProcess htmlAgilityPackageProcess,
        IOptions<ErrorSettings> errorOptions,
        ILogger<ExtractorAmazonProcess> logger)
        : base(
            htmlAgilityPackageProcess,
            errorOptions)
    {
        _htmlAgilityPackageProcess = htmlAgilityPackageProcess;
        _errorSettings = errorOptions.Value;
        _logger = logger;
    }

    public async Task<List<AmazonOffer>> ExtractAmazonOffersAsync(
        List<HtmlNode> htmlNodes,
        string nodeNameTitle,
        string nodeNameDiscount)
    {
        List<AmazonOffer> offers = new();

        foreach (var htmlNode in htmlNodes)
        {
            try
            {
                offers.Add(await ExtractAmazonOfferAsync(
                    htmlNode,
                    nodeNameTitle,
                    nodeNameDiscount)
                );
            }
            catch (Exception ex)
            {
                string message = string.Format(
                    _errorSettings.OfferNoCompleted,
                    ex.StackTrace,
                    ex.Message
                );

                _logger.LogError(message);
            }
        }

        return offers;
    }

    public async Task<AmazonOffer> ExtractAmazonOfferAsync(
        HtmlNode htmlNode,
        string nodeNameTitle,
        string nodeNameDiscount)
    {
        AmazonOffer offer = new()
        {
            Disccount = await ExtractDisccountAsync(
                htmlNode,
                nodeNameDiscount
            ),
            Uri = await ExtractUriAsync(htmlNode),
            Title = await ExtractTextAsync(
                htmlNode,
                nodeNameTitle
            ),
            Img = await ExtractImgAsync(htmlNode)
        };

        return offer;
    }

    public async Task<int?> ExtractAmazonCouponAsync(
        HtmlNode htmlNode,
        string nodeNameCoupon)
    {
        int? coupon = null;

        HtmlNode node = await _htmlAgilityPackageProcess.HtmlNodeSelectSingleNode(
            htmlNode,
            nodeNameCoupon
        );

        if (node != null)
        {
            coupon = int.Parse(Regex.Match(node.InnerText, @"\d+").Value);
        }

        return coupon;
    }

    public async Task<decimal?> ExtractAmazonPriceWithoutDiscountAsync(
        HtmlNode htmlNode,
        string nodeNameParentNoPriceDiscount,
        string nodeNameNoPriceDiscount)
    {
        decimal? price = null;
        HtmlNode parentPriceNode = await _htmlAgilityPackageProcess.HtmlNodeSelectSingleNode(
            htmlNode,
            nodeNameParentNoPriceDiscount
        );

        if (parentPriceNode != null)
        {
            price = await ExtractPriceAsync(
                parentPriceNode,
                nodeNameNoPriceDiscount
            );
        }

        return price;
    }
}
