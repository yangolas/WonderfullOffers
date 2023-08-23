using WonderfullOffers.Domain.Contracts.Domain.Processors;

namespace WonderfullOffers.Domain.Contracts.Domain.UpdateOffers;

public interface IUpdateOfferService : ITransientDomain
{
    Task RepositoriesToUpdateAsync();

    Task UpdateByProcessCompanyAsync(IProcessCompanyBase company);

}