using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonContract;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.Base;

//public interface IAmazonPageMain : IAmazonCreateAndConfigurePageSon, IAmazonProcessPageUris, IAmazonSetUri, IDisposable { }
public interface IAmazonPageMain : IAmazonCreateAndConfigurePageSon, IAmazonProcessPageLinksAndImgs, IAmazonSetUri { void Close(); }