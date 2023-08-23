using HtmlAgilityPack;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.ExtractorProcessBase;

public interface IExtractorProcess : ITransientDomain
{
    Task<string> ExtractAttributeValueAsync(
        HtmlNode htmlNode,
        string nameAttribute);

    Task<int> ExtractDisccountAsync(
        HtmlNode htmlNode,
        string nodeNameDiscount);

    Task<Uri> ExtractImgAsync(HtmlNode htmlNode);

    Task<decimal> ExtractPriceAsync(
        HtmlNode htmlNode,
        string nodeNamePriceDiscount);

    Task<Uri> ExtractUriAsync(
        HtmlNode htmlNode,
        string mainDomainUri = "");

    Task<List<Uri>> ExtractUrisAsync(
        List<HtmlNode> htmlNodes,
        string mainDomainUri = "");

    Task<string> ExtractTextAsync(
        HtmlNode htmlNode,
        string nodeNameTitle);
}