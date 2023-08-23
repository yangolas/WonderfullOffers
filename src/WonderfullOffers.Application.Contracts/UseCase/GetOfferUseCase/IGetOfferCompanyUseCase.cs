using WonderfullOffers.Infraestructure.Contracts.Models;

namespace WonderfullOffers.Application.Contracts.UseCase.GetOfferUseCase;

public interface IGetOfferCompanyUseCase : ITransientUseCase
{
    Task<int> GetNumberOffersCompanyAsync(string type);
    Task<List<IOfferEntity>> GetOffersCompanyAsync(
        string type,
        int paginationFrontEnd);
}