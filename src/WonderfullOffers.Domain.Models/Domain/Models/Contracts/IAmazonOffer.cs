namespace WonderfullOffers.Domain.Models.Domain.Models.Contracts;

public interface IAmazonOffer : IOffer
{
    int? Coupon { get; set; }
}
