using HtmlAgilityPack;
using WonderfullOffers.Domain.Contracts.Domain.Processors.ExtractorProcessBase;
using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;

public interface IExtractorAmazonProcess : IExtractorProcess
{
    Task<List<AmazonOffer>> ExtractAmazonOffersAsync
       (List<HtmlNode> htmlNodes,
       string nodeNameAtribute,
       string nodeNameDiscount);

    Task<AmazonOffer> ExtractAmazonOfferAsync(
        HtmlNode htmlNode,
        string nodeNameAtribute,
        string nodeNameDiscount);

    Task<int?> ExtractAmazonCouponAsync(
        HtmlNode htmlNode,
        string nodeNameCoupon);

    Task<decimal?> ExtractAmazonPriceWithoutDiscountAsync(
        HtmlNode htmlNode,
        string nodeNameParentNoPriceDiscount,
        string nodeNameNoPriceDiscount);
}
