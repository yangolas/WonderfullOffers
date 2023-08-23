using WonderfullOffers.Domain.Models.Domain.Models.Contracts;

namespace WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;

public class AmazonOffer : IAmazonOffer
{
    public int? Coupon { get; set; }
    public string? Title { get; set; }
    public DateTime TimeSpan { get; set; }
    public string? Description { get; set; }
    public Uri Uri { get; set; }
    public Uri? Img { get; set; }
    public int Disccount { get; set; }
    public decimal PriceWithinDisccount { get; set; }
    public decimal? PriceWithoutDisccount { get; set; }
}
