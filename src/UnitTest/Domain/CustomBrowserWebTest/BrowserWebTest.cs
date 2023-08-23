using Castle.Components.DictionaryAdapter.Xml;
using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.BrowserSettings;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;
using WonderfullOffers.Domain.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Domain.Processors.Amazon;
using WonderfullOffers.Tests.UnitTest.Hosting;

namespace WonderfullOffers.Tests.UnitTest.Domain.CustomBrowserWebTest;

public class BrowserWebTest
{
    private readonly Host _host;
    private readonly IBrowserWeb _sut;
    private readonly AmazonSettings _amazonSettings;

    public BrowserWebTest()
    {
        _host = new();
        _sut = new BrowserWeb(
            _host.GetService<IOptions<BrowserSettings>>(),
            _host.GetService<IHtmlAgilityPackageProcess>(),
            _host.GetService<IOptions<ErrorSettings>>()
        );
        _amazonSettings = _host.GetService<IOptions<AmazonSettings>>().Value;
    }

    [Theory]
    [InlineData("https://www.google.com")]
    public async Task Should_Navigate_To_Google_And_Get_Content(string path) 
    {
        HtmlNode pageGoogle = await _sut.NavigateAsync(new Uri(path));

        pageGoogle.Should().NotBeNull();
    }

    [Theory]
    [InlineData("aaaaa")]
    public async Task Should_Throw_Exception_When_Navigate_Inveted_Page(string path)
    {
        Action act = async () => await _sut.NavigateAsync(new Uri(path));

        act.Should().Throw<Exception>();
    }

    [Theory]
    [InlineData("https://www.amazon.es")]
    public async Task Should_Click_In_Cookies_Continue_Without_Cookies(string path)
    {
        HtmlNode pageAmazon = await _sut.NavigateAsync(new Uri(path));

        IFindProcessAmazon findProcessAmazon = _host.GetService<IFindProcessAmazon>();
        string pathNode = await findProcessAmazon.FindPathAsync(pageAmazon, _amazonSettings.Cookies.NodeNameCookies);

        pathNode.Should().NotBeNull();

        pageAmazon = await _sut.ClickAsync(pathNode);

        pathNode = await findProcessAmazon.FindPathAsync(pageAmazon, _amazonSettings.Cookies.NodeNameCookies);

        pageAmazon.Should().BeNull();
    }

    [Theory]
    [InlineData("https://www.amazon.es","click in some place")]
    public async Task Should_Click_In_Some_Place_And_Return_The_Original_Page(string path, string pathClick)
    {
        HtmlNode pageAmazon = await _sut.NavigateAsync(new Uri(path));

        HtmlNode pageAmazonClick = await _sut.ClickAsync(pathClick);

        pageAmazon.OuterHtml.Should().BeEquivalentTo(pageAmazonClick.OuterHtml);
    }

    [Theory]
    [InlineData("https://www.amazon.es")]
    public async Task Should_Click_Arrow_Back_Browser_And_Get_The_Same_Page_Navigation(string path)
    {
        HtmlNode pageAmazon = await _sut.NavigateAsync(new Uri(path));

        HtmlNode pageAmazonClick = await _sut.ClickBackAsync();

        pageAmazon.OuterHtml.Should().BeEquivalentTo(pageAmazonClick.OuterHtml);
    }

    [Theory]
    [InlineData("https://www.amazon.es","https://www.google.com")]
    public async Task Should_Click_Arrow_Back_Browser_And_Back_To_Page_Before(string path, string secondPath)
    {
        HtmlNode pageAmazon = await _sut.NavigateAsync(new Uri(path));

        await _sut.NavigateAsync(new Uri(secondPath));

        HtmlNode pageClickBack = await _sut.ClickBackAsync();

        pageAmazon.OuterHtml.Should().BeEquivalentTo(pageClickBack.OuterHtml);
    }
}