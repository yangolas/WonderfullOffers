using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Repositories;

public class MaquillaliaRepository : CompanyRepository<MaquillaliaOfferEntity>, IMaquillaliaRepository
{
    public MaquillaliaRepository(CompanyContext companyContext)
        : base(companyContext) { }

    public async Task<DateTime> GetLastTimeSpanAsync()
    {
        return await GetLastTimeSpanAsync(maquillalia => maquillalia.TimeSpan);
    }

    public async Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd)
    {
        List<IOfferEntity> maquillaliaOffers = await GetOffersAsync(
            maquillalia => maquillalia.Disccount,
            numberPaginationFrontEnd
        );

        return maquillaliaOffers;
    }
}