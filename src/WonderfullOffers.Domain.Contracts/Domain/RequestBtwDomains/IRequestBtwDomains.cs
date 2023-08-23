using WonderfullOffers.Domain.Contracts;
using WonderfullOffers.Domain.Contracts.Domain.Processors;
using WonderfullOffers.Infraestructure.Contracts.Functionalities;

namespace WonderfullOffers.Domain.Contracts.Domain.RequestBtwDomains;

public interface IRequestBtwDomains : ITransientDomain
{
    public IProcessCompanyBase RequestToProcess(IGenericRepository repository);
}
