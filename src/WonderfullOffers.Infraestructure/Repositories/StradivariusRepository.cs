using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Repositories;

public class StradivariusRepository : CompanyRepository<StradivariusOfferEntity>, IStradivariusRepository
{
    public StradivariusRepository(CompanyContext companyContext)
        : base(companyContext) { }

    public async Task<DateTime> GetLastTimeSpanAsync()
    {
        return await GetLastTimeSpanAsync(stradivarius => stradivarius.TimeSpan);
    }

    public async Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd)
    {
        List<IOfferEntity> stradivariusOffers = await GetOffersAsync(
            stradivarius => stradivarius.Disccount,
            numberPaginationFrontEnd
        );

        return stradivariusOffers;
    }
}
