namespace WonderfullOffers.Infraestructure.Contracts.Models;

public interface IOfferEntity
{
    string Title { get; set; }   
    DateTime TimeSpan { get; set; } 
    Uri Uri { get; set; }
    Uri? Img { get; set; }    
    int Disccount { get; set; }  
    decimal PriceWithinDisccount { get; set; }
    decimal? PriceWithoutDisccount { get; set; }
}