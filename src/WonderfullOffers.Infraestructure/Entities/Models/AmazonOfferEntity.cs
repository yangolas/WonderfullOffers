using WonderfullOffers.Infraestructure.Contracts.Models;

namespace WonderfullOffers.Infraestructure.Entities.Models;

public class AmazonOfferEntity : OfferEntity, IAmazonEntity
{
    public int? Coupon { get; set; }
}