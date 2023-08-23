using HtmlAgilityPack;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.FindProcessBase;

public interface IFindProcess : ITransientDomain
{
    Task<HtmlNode> FindNodeAsync(
        HtmlNode htmlDocument,
        string nodeNameOffer);

    Task<List<HtmlNode>> FindNodesLinksCatalogOffersAsync(
        HtmlNode htmlDocument,
        string nodeNameCatalog,
        List<string> catalogNames);

    Task<List<HtmlNode>> FindNodesAsync(
        HtmlNode htmlDocument,
        string nodeNameOffer);

    Task<bool> HasNodesToLinkAsync(
        HtmlNode htmlNode,
        List<string> catalogNames);

    Task<string?> FindAvailablePaginationPathNextAsync(
        HtmlNode htmlNode,
        string filterNextPagination,
        string nodeNameNextPaginationDisable);

    Task<string?> FindPathAsync(
        HtmlNode htmlNode,
        string nodeNameCookies);
}