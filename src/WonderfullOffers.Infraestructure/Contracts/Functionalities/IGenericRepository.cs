using WonderfullOffers.Infraestructure.Contracts.Models;

namespace WonderfullOffers.Infraestructure.Contracts.Functionalities;

public interface IGenericRepository
{
    Task<int> GetNumberOffersAsync();

    Task<DateTime> GetLastTimeSpanAsync();
    
    Task<List<IOfferEntity>> GetOffersAsync();

    Task<List<IOfferEntity>> GetOffersAsync(int numberPaginationFrontEnd);

    Task InsertOffersAsync(List<IOfferEntity> offersProcesess);
}