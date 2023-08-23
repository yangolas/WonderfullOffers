using HtmlAgilityPack;
using WonderfullOffers.Domain.Contracts.Domain.Processors.FindProcessBase;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;

public interface IFindProcessAmazon : IFindProcess
{
    Task<HtmlNode> GetNodeMultyAsync(HtmlNode nodeOffer);

    Task<List<HtmlNode>> GetNodesMultyAsync(List<HtmlNode> nodeOffers);

    Task<HtmlNode> GetNodeGroupProviderAsync(HtmlNode nodeOffer);

    Task<List<HtmlNode>> GetNodesGroupProviderAsync(List<HtmlNode> nodeOffers);

    Task<List<HtmlNode>> GetNodesGroupTypeS(List<HtmlNode> nodeOffers);
}