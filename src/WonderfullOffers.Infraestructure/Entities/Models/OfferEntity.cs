using System.ComponentModel.DataAnnotations;
using WonderfullOffers.Infraestructure.Contracts.Models;

namespace WonderfullOffers.Infraestructure.Entities.Models;

public abstract class OfferEntity : IOfferEntity
{
    [Key]
    public string Title { get; set; }
    public DateTime TimeSpan { get; set; }
    public Uri Uri { get; set; }
    public Uri? Img { get; set; }
    public int Disccount { get; set; }
    public decimal PriceWithinDisccount { get; set; }
    public decimal? PriceWithoutDisccount { get; set; }

}
