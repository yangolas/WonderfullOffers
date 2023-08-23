using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Repositories;

public class DouglasRepository : CompanyRepository<DouglasOfferEntity>, IDouglasRepository
{
    public DouglasRepository(CompanyContext companyContext)
        : base(companyContext) { }

    public async Task<DateTime> GetLastTimeSpanAsync()
    {
        return await GetLastTimeSpanAsync(douglas => douglas.TimeSpan);
    }

    public async Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd)
    {
        List<IOfferEntity> douglasOffers = await GetOffersAsync(
            douglas => douglas.Disccount,
            numberPaginationFrontEnd
        );

        return douglasOffers;
    }
}