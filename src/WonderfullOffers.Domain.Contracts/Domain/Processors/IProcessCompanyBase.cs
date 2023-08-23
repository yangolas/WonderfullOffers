using WonderfullOffers.Domain.Models.Domain.Models.Contracts;

namespace WonderfullOffers.Domain.Contracts.Domain.Processors;

public interface IProcessCompanyBase : ITransientDomain
{
    Task<List<IOffer>> ProcessOffersCompanyAsync();
}