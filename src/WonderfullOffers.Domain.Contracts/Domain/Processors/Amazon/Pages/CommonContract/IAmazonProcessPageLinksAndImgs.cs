using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;
using WonderfullOffers.Domain.Models.Domain.Models.UriImg;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon.Pages.CommonContract;

public interface IAmazonProcessPageLinksAndImgs
{
    Task<List<LinkAndImg>> ProcessPageLinksAndImgsAsync();
}