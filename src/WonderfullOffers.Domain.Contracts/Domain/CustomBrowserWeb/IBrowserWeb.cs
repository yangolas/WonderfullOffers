using HtmlAgilityPack;

namespace WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;

//public interface IBrowserWeb : IDisposable
public interface IBrowserWeb
{
    Task<HtmlNode> ClickRedirectAsync(string filter, string pathDeleteElement = "");

    Task<HtmlNode> ClickAsync(
        string path,
        string deleteDivIDOverElementclick = "");

    Task<HtmlNode> ClickBackAsync();

    Task<Uri> GetCurrentUrlAsync();

    Task<IBrowserWeb> NewBrowserWithMyConfigAsync();

    Task<HtmlNode> NavigateAsync(Uri uri);
    void Close();
}