using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Domain.Processors.FindProcessBase;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;

namespace WonderfullOffers.Domain.Domain.Processors.FindProcessBase;

public class FindProcess : IFindProcess
{
    private readonly IHtmlAgilityPackageProcess _htmlAgilityPackageProcess;
    private readonly ErrorSettings _errorSettings;
    private readonly ILogger<FindProcess> _logger;

    public FindProcess(
        IHtmlAgilityPackageProcess htmlAgilityPackageProcess,
        IOptions<ErrorSettings> optionsError,
        ILogger<FindProcess> logger)
    {
        _htmlAgilityPackageProcess = htmlAgilityPackageProcess;
        _errorSettings = optionsError.Value;
        _logger = logger;
    }

    public async Task<List<HtmlNode>> FindNodesLinksCatalogOffersAsync(
        HtmlNode htmlNode,
        string nodeNameCatalog,
        List<string> catalogNames)
    {
        //Find the possibles nodes that contains redirects links
        List<HtmlNode> nodes = await _htmlAgilityPackageProcess.HtmlNodeSelectNodes(
          htmlNode,
          nodeNameCatalog
        );

        List<HtmlNode> divsToGetRedirect = new();
        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                if (await HasNodesToLinkAsync(
                        node,
                        catalogNames)
                    )
                {
                    divsToGetRedirect.Add(node);
                }
            }
        }

        return divsToGetRedirect;
    }

    public async Task<List<HtmlNode>> FindNodesAsync(
        HtmlNode htmlNode,
        string nodeNameOffer)
    {
        List<HtmlNode> nodes = await _htmlAgilityPackageProcess.HtmlNodeSelectNodes(
            htmlNode,
            nodeNameOffer
       );

        return nodes;
    }

    public async Task<HtmlNode> FindNodeAsync(
        HtmlNode HtmlNode,
        string nodeName)
    {
        HtmlNode node = await _htmlAgilityPackageProcess.HtmlNodeSelectSingleNode(
           HtmlNode,
           nodeName
       );

        return node;
    }

    public async Task<bool> HasNodesToLinkAsync(
        HtmlNode htmlNode,
        List<string> catalogNames)
    {
        string htmlNodeText = htmlNode.InnerText.ToLower();

        return await Task.Run(() => catalogNames.Any(
            keyword => htmlNodeText.Contains(keyword.ToLower()
                )
            ));
    }

    public async Task<string?> FindAvailablePaginationPathNextAsync(
        HtmlNode htmlNode,
        string filterNextPagination,
        string nodeNameNextPaginationDisable
        )
    {
        HtmlNode nodeNext;

        try
        {
            nodeNext = await FindNodeAsync(
                htmlNode,
                filterNextPagination
            );
        }
        catch (Exception ex)
        {
            string message = string.Format(
                _errorSettings.PaginationNotFound,
                ex.Message
            );

            _logger.LogError(message);

            return null;
        }

        try
        {
            HtmlNode nodePagination = await FindNodeAsync(
                    nodeNext,
                    nodeNameNextPaginationDisable
                );
        }
        //If catch no worries continue navigation for pages
        catch (Exception)
        {
            return nodeNext.XPath;
        }

        //No more offers
        return null;
    }

    public async Task<string?> FindPathAsync(
       HtmlNode htmlNode,
       string nodeName)
    {
        HtmlNode node;
        try
        {
            node = await FindNodeAsync(
                htmlNode,
                nodeName
            );
        }
        //for cookies:If catch no worries div in middle of the screen is deactived
        //for select department: receive null
        catch (Exception)
        {
            return null;
        }

        return node.XPath;
    }

}