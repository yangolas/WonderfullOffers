namespace WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;

public class AmazonSettings
{
    public int NumberOfTasksRunning { get; set; }
    public string MainDomain { get; set; }
    public List<string> AmazonUrls { get; set; }
    public PageGroupProviderAndMultyAndTypeS PageGroupProviderAndMultyAndTypeS { get; set; }
    public PageProvider PageProvider { get; set; }
    public PageTypologyS pageTypologyS { get; set; }
    public PageSpecific PageSpecific { get; set; }
    public Pagination Pagination { get; set; }
    public Cookies Cookies { get; set; }
}