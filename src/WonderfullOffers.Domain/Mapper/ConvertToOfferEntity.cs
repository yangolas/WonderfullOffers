using Microsoft.Extensions.Options;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Mappers;
using WonderfullOffers.Domain.Domain.CustomException;
using WonderfullOffers.Domain.Models.Domain.Models.Contracts;
using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;
using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Domain.Mapper;

public class ConvertToOfferEntity : IConvertToOfferEntity
{
    private readonly ErrorSettings _errorSettings;

    public ConvertToOfferEntity(IOptions<ErrorSettings> optionError)
    {
        _errorSettings = optionError.Value;
    }

    public List<IOfferEntity> Convert(List<IOffer> offers)
    {
        string nameDomain = offers.First().GetType().Name; ;
        switch (nameDomain)
        {
            case nameof(AmazonOffer):
                {
                    List<IAmazonOffer> amazonOffers  = TransformGenricOfferToSpecific<IAmazonOffer>(offers);

                    List<AmazonOfferEntity> amazonEntityOffers = CreateAmazonEntity(amazonOffers);

                    return TransformSpecificOfferToGeneralEntity(amazonEntityOffers);
                }

            default:
                throw new ArgumentException(string.Format(
                        _errorSettings.ParseOfferNotFound,
                        StackTree.GetPathError(new StackTrace(true)),
                        nameDomain
                    )
                );
        }
    }

    private List<TOfferCompanyEntity> TransformGenricOfferToSpecific<TOfferCompanyEntity>(List<IOffer> offers) where TOfferCompanyEntity : class 
    {
        return offers.Cast<TOfferCompanyEntity>().ToList();
    }

    private List<IOfferEntity> TransformSpecificOfferToGeneralEntity<TOfferCompanyEntity>(List<TOfferCompanyEntity> offers) where TOfferCompanyEntity : class
    {
        return offers.Cast<IOfferEntity>().ToList();
    }
    
    private List<AmazonOfferEntity> CreateAmazonEntity(List<IAmazonOffer> amazonOffers) 
        => amazonOffers.Select(offer =>
            new AmazonOfferEntity()
            {
                Coupon = offer.Coupon,
                Title = offer.Title,
                TimeSpan = offer.TimeSpan,
                Uri = offer.Uri,
                Img = offer.Img,
                Disccount = offer.Disccount,
                PriceWithinDisccount = offer.PriceWithinDisccount,
                PriceWithoutDisccount = offer.PriceWithoutDisccount
            }
        ).ToList();
}