using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Repositories;

public class SheinRepository : CompanyRepository<SheinOfferEntity>, ISheinRepository
{
    public SheinRepository(CompanyContext companyContext)
        : base(companyContext) { }

    public async Task<DateTime> GetLastTimeSpanAsync()
    {
        return await GetLastTimeSpanAsync(shein => shein.TimeSpan);
    }

    public async Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd)
    {
        List<IOfferEntity> sheinOffers = await GetOffersAsync(
            shein => shein.Disccount,
            numberPaginationFrontEnd
        );

        return sheinOffers;
    }
}
