using HtmlAgilityPack;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;

public interface IHtmlAgilityPackageProcess : ITransientDomain
{
    Task<HtmlNode> LoadPageStringAsync(string pageContent);

    Task<List<HtmlNode>> HtmlNodeSelectNodes(
        HtmlNode htmlNode,
        string filter);

    Task<HtmlNode> HtmlNodeSelectSingleNode(
        HtmlNode htmlNode,
        string filter);

    Task<string> HtmlNodeGetAttributeValueForDiv(
        HtmlNode htmlNode,
        string name);

    Task<string> HtmlNodeGetAttributeValueForImg(
        HtmlNode htmlNode);
}