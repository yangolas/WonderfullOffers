using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Repositories;

public class PrimorRepository : CompanyRepository<PrimorOfferEntity>, IPrimorRepository
{
    public PrimorRepository(CompanyContext companyContext)
        : base(companyContext) { }

    public async Task<DateTime> GetLastTimeSpanAsync()
    {
        return await GetLastTimeSpanAsync(primor => primor.TimeSpan);
    }

    public async Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd)
    {
        List<IOfferEntity> primorOffers = await GetOffersAsync(
            primor => primor.Disccount,
            numberPaginationFrontEnd
        );

        return primorOffers;
    }
}