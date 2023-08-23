using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Repositories;

public class SpringfieldRepository : CompanyRepository<SpringfieldOfferEntity>, ISpringfieldRepository
{
    public SpringfieldRepository(CompanyContext companyContext)
        : base(companyContext) { }

    public async Task<DateTime> GetLastTimeSpanAsync()
    {
        return await GetLastTimeSpanAsync(springfield => springfield.TimeSpan);
    }

    public async Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd)
    {
        List<IOfferEntity> springfieldOffers = await GetOffersAsync(
            springfield => springfield.Disccount,
            numberPaginationFrontEnd
        );

        return springfieldOffers;
    }
}