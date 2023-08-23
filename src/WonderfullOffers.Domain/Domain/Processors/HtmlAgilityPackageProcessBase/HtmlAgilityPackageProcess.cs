using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;
using WonderfullOffers.Domain.Domain.CustomException;

namespace WonderfullOffers.Domain.Domain.Processors.HtmlAgilityPackageProcessBase;

public class HtmlAgilityPackageProcess : IHtmlAgilityPackageProcess
{
    private readonly ErrorSettings _errorSettings;
    public HtmlAgilityPackageProcess(IOptions<ErrorSettings> errorOptions)
    {
        _errorSettings = errorOptions.Value;
    }

    public async Task<List<HtmlNode>> HtmlNodeSelectNodes(
        HtmlNode htmlNode,
        string filter)
    {
        try
        {
            return await Task.Run(() =>
                htmlNode.SelectNodes(filter)?
                .ToList()
                ?? throw new NullReferenceException(
                    string.Format(
                        _errorSettings.FilterNotFoundContent,
                        StackTree.GetPathError(new StackTrace(true)),
                        filter,
                        htmlNode.OuterHtml
                    )
                )
            );
        }
        catch (NullReferenceException ex)
        {
            throw new NullReferenceException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new ArgumentException(
                string.Format(
                    _errorSettings.FilterNotValid,
                    StackTree.GetPathError(new StackTrace(true)),
                    filter,
                    htmlNode.OuterHtml
                )
            );
        }
    }

    public async Task<HtmlNode> HtmlNodeSelectSingleNode(
        HtmlNode htmlNode,
        string filter)
    {
        try
        {
            return await Task.Run(() =>
                htmlNode.SelectSingleNode(filter)
                ?? throw new NullReferenceException(
                    string.Format(
                        _errorSettings.FilterNotFoundContent,
                        StackTree.GetPathError(new StackTrace(true)),
                        filter,
                        htmlNode.OuterHtml
                    )
                )
            );
        }
        catch (NullReferenceException ex)
        {
            throw new NullReferenceException(ex.Message);
        }
        catch (Exception)
        {
            throw new ArgumentException(
                string.Format(
                    _errorSettings.FilterNotValid,
                    StackTree.GetPathError(new StackTrace(true)),
                    filter,
                    htmlNode.OuterHtml
                )
            );
        }
    }
    private async Task<string> HtmlNodeGetAttributeValue(
        HtmlNode htmlNode,
        string name)
    {
        try
        {
            return await Task.Run(() =>
                    htmlNode.GetAttributeValue(name, null)
                    ?? throw new NullReferenceException(
                    string.Format(
                        _errorSettings.FilterNotFoundContent,
                        StackTree.GetPathError(new StackTrace(true)),
                        name,
                        htmlNode.OuterHtml
                    )
                )
            );
        }
        catch (NullReferenceException ex)
        {
            throw new NullReferenceException(ex.Message);
        }
        catch (Exception)
        {
            throw new ArgumentException(
                string.Format(
                    _errorSettings.FilterAttributeNoValid,
                   StackTree.GetPathError(new StackTrace(true)),
                    name,
                    htmlNode.OuterHtml
                )
            );
        }
    }

    public async Task<string> HtmlNodeGetAttributeValueForDiv(
        HtmlNode htmlNode,
        string name)
    {
        HtmlNode node = await HtmlNodeSelectSingleNode(
            htmlNode,
            "//div"
        );

        return await HtmlNodeGetAttributeValue(
            node,
            name
        );
    }

    public async Task<string> HtmlNodeGetAttributeValueForImg(
        HtmlNode htmlNode)
    {
        return await HtmlNodeGetAttributeValue(
            htmlNode,
            "src"
        );
    }

    public async Task<HtmlNode> LoadPageStringAsync(string pageContent)
    {
        try
        {
            HtmlDocument htmlDocument = new();

            await Task.Run(() =>
                htmlDocument.LoadHtml(pageContent)
            );

            return htmlDocument.DocumentNode;
        }
        catch (Exception)
        {
            throw new ArgumentException(
                string.Format(
                    _errorSettings.ErrorToLoadHtml,
                    StackTree.GetPathError(new StackTrace(true)),
                    pageContent
                )
            );
        }
    }
}
