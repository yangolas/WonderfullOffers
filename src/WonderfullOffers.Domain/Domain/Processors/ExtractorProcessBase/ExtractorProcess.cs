using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Domain.Processors.ExtractorProcessBase;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;

namespace WonderfullOffers.Domain.Domain.Processors.ExtractorProcessBase;

public class ExtractorProcess : IExtractorProcess
{
    private readonly IHtmlAgilityPackageProcess _htmlAgilityPackageProcess;
    private readonly ErrorSettings _errorSettings;

    public ExtractorProcess(
        IHtmlAgilityPackageProcess htmlAgilityPackageProcess,
        IOptions<ErrorSettings> errorSettings)
    {
        _htmlAgilityPackageProcess = htmlAgilityPackageProcess;
        _errorSettings = errorSettings.Value;
    }

    public async Task<string> ExtractAttributeValueAsync(
        HtmlNode htmlNode,
        string nameAttribute)
    {
        string value = await _htmlAgilityPackageProcess.HtmlNodeGetAttributeValueForDiv(
            htmlNode,
            nameAttribute
        );

        return value;
    }

    public async Task<int> ExtractDisccountAsync(
        HtmlNode htmlNode,
        string nodeNameDiscount)
    {
        HtmlNode disscountNode = await _htmlAgilityPackageProcess.HtmlNodeSelectSingleNode(
            htmlNode,
            nodeNameDiscount
        );

        string discountText = Regex.Match(disscountNode.InnerText, @"\d+([,.]\d+)?").Value;

        int disccountResult;

        if (!discountText.IsNullOrEmpty() && int.TryParse(discountText, out int disccount))
        {
            disccountResult = disccount;
        }
        else
        {
            throw new Exception(
                string.Format(
                    _errorSettings.DisccountNotValid,
                    nodeNameDiscount,
                    discountText,
                    htmlNode.OuterHtml
                )
            );
        }

        return disccountResult;
    }

    public async Task<Uri> ExtractImgAsync(HtmlNode htmlNode)
    {
        HtmlNode imgNode = await _htmlAgilityPackageProcess.HtmlNodeSelectSingleNode(
            htmlNode,
            ".//img"
        );

        string imgString = await _htmlAgilityPackageProcess.HtmlNodeGetAttributeValueForImg(
            imgNode
        );

        return new Uri(imgString);
    }

    public async Task<decimal> ExtractPriceAsync(
        HtmlNode htmlNode,
        string nodeNamePrice)
    {

        HtmlNode priceNode = await _htmlAgilityPackageProcess.HtmlNodeSelectSingleNode(
            htmlNode,
            nodeNamePrice
        );

        string priceText = Regex.Match(priceNode.InnerText, @"\d+([,.]\d+)?").Value;

        decimal priceResult;

        if (!priceText.IsNullOrEmpty()
            && decimal.TryParse(
                priceText,
                NumberStyles.Number,
                new CultureInfo("es-ES"),
                out decimal price
            )
        )
        {
            priceResult = price;
        }
        else
        {
            throw new Exception(
                string.Format(
                    _errorSettings.PriceNotValid,
                    nodeNamePrice,
                    priceText,
                    htmlNode.OuterHtml
                )
            );
        }

        return priceResult;
    }

    public async Task<string> ExtractTextAsync(HtmlNode htmlNode, string nodeNameTitle)
    {
        HtmlNode textNode = await _htmlAgilityPackageProcess.HtmlNodeSelectSingleNode(
            htmlNode,
            nodeNameTitle
        );

        string text = textNode.InnerText;

        return text;
    }

    public async Task<Uri> ExtractUriAsync(HtmlNode htmlNode, string mainDomainUri = "")
    {
        HtmlNode node = await _htmlAgilityPackageProcess.HtmlNodeSelectSingleNode(
            htmlNode,
            ".//a[@href]"
        );

        string urlDecoded = HttpUtility.HtmlDecode($"{mainDomainUri}{node.Attributes["href"].Value}");
        return new Uri(urlDecoded);
    }

    public async Task<List<Uri>> ExtractUrisAsync(List<HtmlNode> htmlNodes, string mainDomainUri = "")
    {
        IEnumerable<Task<Uri>> taskUris = htmlNodes
            .Select(htmlNode =>
                ExtractUriAsync(htmlNode, mainDomainUri)
            );

        List<Uri> uriResults = (await Task.WhenAll(taskUris)).ToList();

        return uriResults;
    }
}
