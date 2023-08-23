using System.Linq.Expressions;
using WonderfullOffers.Infraestructure.Contracts.Models;

namespace WonderfullOffers.Infraestructure.Contracts.Repository;

public interface ICompanyRepository<TEntity> where TEntity : class
{
    Task<int> GetNumberOffersAsync();
    Task<DateTime> GetLastTimeSpanAsync(Expression<Func<TEntity, DateTime>> entityFieldTime);

    Task<List<IOfferEntity>> GetOffersAsync();

    Task<List<IOfferEntity>> GetOffersAsync(
        Expression<Func<TEntity, int>> orderByProperty,
        int numberPaginationFrontEnd);

    Task InsertOffersAsync(List<IOfferEntity> offersProcesess);
}