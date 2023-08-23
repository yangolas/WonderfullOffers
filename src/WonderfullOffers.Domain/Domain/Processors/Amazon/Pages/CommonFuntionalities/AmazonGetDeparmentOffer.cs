using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonFunctionality;
using WonderfullOffers.Domain.Domain.CustomException;

namespace WonderfullOffers.Domain.Domain.Processors.Amazon.Pages.CommonFuntionalities;

public class AmazonGetDeparmentOffer : IAmazonGetDeparmentOffer
{
    private readonly AmazonSettings _amazonSettings;
    private readonly IFindProcessAmazon _findProcessAmazon;
    private readonly ErrorSettings _errorSetting;

    public AmazonGetDeparmentOffer(
        IOptions<AmazonSettings> amazonSettings,
        IFindProcessAmazon findProcessAmazon,
        IOptions<ErrorSettings> optionError)
    {
        _amazonSettings = amazonSettings.Value;
        _findProcessAmazon = findProcessAmazon;
        _errorSetting = optionError.Value;
    }

    public async Task<HtmlNode> GetClickDepartmentsAsync(
        HtmlNode htmlNode,
        IBrowserWeb browserWeb)
    {
        HtmlNode lastPage = null;
        HtmlNode deparmentNode = await _findProcessAmazon.FindNodeAsync(
            htmlNode,
            _amazonSettings.PageGroupProviderAndMultyAndTypeS.NodeNameDeparmentOffers
        );


        foreach (string department in _amazonSettings.PageGroupProviderAndMultyAndTypeS.OfferSections)
        {
            string path = string.Empty;
            try
            {
                path = await _findProcessAmazon.FindPathAsync(
                    deparmentNode,
                    department
                )
                ?? throw new NullReferenceException(
                    string.Format(
                        _errorSetting.FilterNotValid,
                        StackTree.GetPathError(new StackTrace(true)),
                        department,
                        htmlNode.OuterHtml
                    )
                );
                lastPage = await browserWeb.ClickAsync(path);
            }
            catch (Exception) { }
        }

        return lastPage ?? throw new NullReferenceException(
            string.Format(
                _errorSetting.FilterNotValid,
                StackTree.GetPathError(new StackTrace(true)),
                "Browser Doesnt work",
                htmlNode.OuterHtml
            )
        );
    }
}
