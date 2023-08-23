using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.BrowserSettings;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Domain.CustomBrowserWeb;
using WonderfullOffers.Domain.Contracts.Domain.ProcessOperativeSystem;
using WonderfullOffers.Domain.Contracts.Domain.Processors.HtmlAgilityPackageProcessBase;
using WonderfullOffers.Domain.Domain.ProcessOperativeSystem;

namespace WonderfullOffers.Domain.Domain.CustomBrowserWeb;

public class BrowserWeb : IBrowserWeb
{
    private readonly IHtmlAgilityPackageProcess _htmlAgilityPackageProcess;
    private readonly IOptions<BrowserSettings> _optionBrowser;
    private readonly IOptions<ErrorSettings> _optionError;

    private readonly IWebDriver _browser;
    private readonly  BrowserSettings _browserSettings;
    public readonly ErrorSettings _errorSettings;
    private readonly string _flag = "fatherprocessguid-";
    private readonly string _processName = "chrome";
    private readonly string _filter;
    private readonly List<Process> _processes = new();
    //private readonly IProcessOS _processOS;

    public BrowserWeb(
        IOptions<BrowserSettings> optionsBrowserSettings,
        IHtmlAgilityPackageProcess htmlAgilityPackageProcess,
        IOptions<ErrorSettings> optionsError)
    {
        _htmlAgilityPackageProcess = htmlAgilityPackageProcess;
        _optionBrowser = optionsBrowserSettings;
        _optionError = optionsError;

        _errorSettings = optionsError.Value;
        _browserSettings = optionsBrowserSettings.Value;

        ChromeOptions options = new ChromeOptions();

        options.AddArgument(_browserSettings.Agent);
        options.AddArgument(_browserSettings.InterfazMode);
        options.AddArgument(_browserSettings.UserInterfaze);
        options.AddArgument(_browserSettings.Javascript);
        options.AddArgument(_browserSettings.Javascript);

        string guid = Guid.NewGuid().ToString();
        _filter = $"{_flag}{guid}";
        options.AddArgument(_filter);

        ChromeDriverService service = ChromeDriverService.CreateDefaultService(options);

        _browser = new ChromeDriver(service, options);

        //_processOS = new ProcessOS(optionsError);
        //_processes.Add(_processOS.GetProcessById(service.ProcessId));
        //_processes.AddRange(_processOS.GetProcesses(_processName, _filter));
    }

    public async Task<HtmlNode> NavigateAsync(Uri uri)
    {
        try
        {
            await Task.Run(() =>
                {
                    _browser.Navigate().GoToUrl(uri);
                    Thread.Sleep(600);
                }
            );

            return await _htmlAgilityPackageProcess
                .LoadPageStringAsync(_browser.PageSource);
        }
        catch (Exception ex)
        {
            throw new Exception(
                string.Format
                (
                    _errorSettings.UrlBrowserNotFound,
                    ex.StackTrace,
                    uri.OriginalString
                )
            );
        }
    }

    public async Task<HtmlNode> ClickRedirectAsync(
        string path,
        string deleteDivIDOverElementclick = "")
    {
        return await ClickAsync(
            path,
            deleteDivIDOverElementclick
        );
    }

    public async Task<HtmlNode> ClickAsync(
        string path,
        string deleteDivIDOverElementclick = "")
    {
        try
        {
            await Task.Run( () =>
                {
                    IWebElement node = _browser.FindElement(By.XPath($"{path}"));

                    if (!deleteDivIDOverElementclick.IsNullOrEmpty())
                    {
                        IReadOnlyCollection<IWebElement> divs = _browser
                        .FindElements(By.TagName("div"));

                        IWebElement? deleteNode = divs.FirstOrDefault(element =>
                        element.GetAttribute("id") == deleteDivIDOverElementclick);

                        if (deleteNode != null)
                            ((IJavaScriptExecutor)_browser)
                                .ExecuteScript("arguments[0].style.display = 'none';", deleteNode);
                    }

                    node.Click();

                    //Waiting to load items in web
                    Thread.Sleep(800);
                }
            );
        }
        catch (Exception) { return null; }

        return await _htmlAgilityPackageProcess
                    .LoadPageStringAsync(_browser.PageSource);
    }

    public async Task<HtmlNode> ClickBackAsync()
    {
        try 
        { 
            await Task.Run(() => 
                {
                    _browser.Navigate().Back();
                    Thread.Sleep(400);
                }
            );
        }
        catch (Exception) { }

        return await _htmlAgilityPackageProcess
            .LoadPageStringAsync(_browser.PageSource);
    }

    public async Task<Uri> GetCurrentUrlAsync()
    {
        return await Task.Run(() =>
            new Uri(_browser.Url)
        );
    }

    private async Task CloseBrowserAsync()
    {
            await Task.Run(() =>
            {
                _browser.Quit();
                _browser.Dispose();
                //_processOS.KillProcess(_processes);
            });
    }

    public async Task<IBrowserWeb> NewBrowserWithMyConfigAsync()
    {
        return await Task.Run(() =>
            {
                return new BrowserWeb(
                    _optionBrowser,
                    _htmlAgilityPackageProcess,
                    _optionError);
            }
        );
    }

    public void Close()
    {
        Task.Run(async () =>
            {
                await  CloseBrowserAsync();
            }
        );
    }
}