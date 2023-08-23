using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Repositories;

public class ZalandoRepository : CompanyRepository<ZalandoOfferEntity>, IZalandoRepository
{
    public ZalandoRepository(CompanyContext companyContext)
        : base(companyContext) { }

    public async Task<DateTime> GetLastTimeSpanAsync()
    {
        return await GetLastTimeSpanAsync(zalando => zalando.TimeSpan);
    }

    public async Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd)
    {
        List<IOfferEntity> zalandoOffers = await GetOffersAsync(
            zalando => zalando.Disccount,
            numberPaginationFrontEnd
        );

        return zalandoOffers;
    }
}