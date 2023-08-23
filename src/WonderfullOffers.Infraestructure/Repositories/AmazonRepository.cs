using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Repositories;

public class AmazonRepository : CompanyRepository<AmazonOfferEntity>, IAmazonRepository
{
    public AmazonRepository(CompanyContext companyContext)
        : base(companyContext) { }

    public async Task<DateTime> GetLastTimeSpanAsync()
    {
        return await GetLastTimeSpanAsync(amazon => amazon.TimeSpan);
    }

    public async Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd)
    {
        List<IOfferEntity> amazonOffers = await GetOffersAsync(
            amazon => amazon.Disccount,
            numberPaginationFrontEnd
        );

        return amazonOffers;
    }
}