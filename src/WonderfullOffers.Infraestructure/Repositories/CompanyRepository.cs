using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities;

namespace WonderfullOffers.Infraestructure.Repositories
{
    public abstract class CompanyRepository<TEntity> : ICompanyRepository<TEntity> where TEntity : class
    {
        private CompanyContext _companyContext { get; set; }
        private DbSet<TEntity> _dbSet { get; set; }

        public CompanyRepository(CompanyContext companyContext)
        {
            _companyContext = companyContext;
            _dbSet = _companyContext.Set<TEntity>();
        }

        public async Task<DateTime> GetLastTimeSpanAsync(Expression<Func<TEntity, DateTime>> entityFieldTime)
        {
            DateTime maxTimeSpan = await _dbSet.MaxAsync(entityFieldTime);

            return maxTimeSpan;
        }

        public async Task<int> GetNumberOffersAsync()
        {
            int numberOffers = await _dbSet.CountAsync();

            return numberOffers;
        }

        public async Task<List<IOfferEntity>> GetOffersAsync()
        {
            List<TEntity> offers = await _dbSet
                .ToListAsync();

            return Convert(offers);
        }

        public async Task<List<IOfferEntity>> GetOffersAsync(
            Expression<Func<TEntity, int>> orderByProperty,
            int numberPaginationFrontEnd)
        {
            List<TEntity> offers = await _dbSet
                .OrderByDescending(orderByProperty)
                .Skip(numberPaginationFrontEnd)
                .Take(60)
                .ToListAsync();

            return Convert(offers);
        }

        public virtual async Task InsertOffersAsync(List<IOfferEntity> newOffers) 
        {
            List<TEntity> offers = Convert(newOffers);

            await _dbSet.AddRangeAsync(offers);

            await _companyContext.SaveChangesAsync();
        }

        private List<IOfferEntity> Convert(List<TEntity> offers) 
        {
            return offers.Cast<IOfferEntity>().ToList();
        }

        private List<TEntity> Convert(List<IOfferEntity> offers)
        {
            return offers.Cast<TEntity>().ToList();
        }
    }
}