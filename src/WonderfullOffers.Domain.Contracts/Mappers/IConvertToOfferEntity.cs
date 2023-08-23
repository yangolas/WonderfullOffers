using WonderfullOffers.Domain.Models.Domain.Models.Contracts;
using WonderfullOffers.Infraestructure.Contracts.Models;

namespace WonderfullOffers.Domain.Contracts.Mappers;

public interface IConvertToOfferEntity : ITransientDomain
{
    public List<IOfferEntity> Convert(List<IOffer> offers);
}