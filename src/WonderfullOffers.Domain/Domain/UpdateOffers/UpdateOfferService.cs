using Microsoft.Extensions.Logging;
using WonderfullOffers.Domain.Contracts.Domain.Processors;
using WonderfullOffers.Domain.Contracts.Domain.RequestBtwDomains;
using WonderfullOffers.Domain.Contracts.Domain.UpdateOffers;
using WonderfullOffers.Domain.Contracts.Mappers;
using WonderfullOffers.Domain.Models.Domain.Models.Contracts;
using WonderfullOffers.Infraestructure.Contracts.Functionalities;
using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;

namespace WonderfullOffers.Domain.Domain.UpdateOffers;

public class UpdateOfferService : IUpdateOfferService
{

    private readonly List<IGenericRepository> _companiesRepository;
    private readonly IConvertToOfferEntity _mapperToOfferEntity;
    private readonly ILogger<UpdateOfferService> _logger;
    private readonly IRequestBtwDomains _requestBtwDomains;
    private IGenericRepository? _companySelectedRespository;

    public UpdateOfferService(
        IAmazonRepository amazonRepository,
        IDouglasRepository douglasRepository,
        IDruniRepository druniRepository,
        IMaquillaliaRepository maquillaliaRepository,
        IPrimorRepository primorRepository,
        ISheinRepository sheinRepository,
        ISpringfieldRepository springfieldRepository,
        IStradivariusRepository stradivariusRepository,
        IWomensecretRepository womensecretRepository,
        IZalandoRepository zalandoRepository,
        ILogger<UpdateOfferService> logger,
        IRequestBtwDomains requestBtwDomains,
        IConvertToOfferEntity mapperToOfferEntity
        )
    {
        _requestBtwDomains = requestBtwDomains;
        _mapperToOfferEntity = mapperToOfferEntity;
        _logger = logger;
        _companiesRepository = new()
        {
            amazonRepository,
            douglasRepository,
            druniRepository,
            maquillaliaRepository,
            primorRepository,
            sheinRepository,
            springfieldRepository,
            stradivariusRepository,
            womensecretRepository,
            zalandoRepository
        };

    }

    public async Task RepositoriesToUpdateAsync()
    {
        foreach (var companyRepo in _companiesRepository)
        {
            try
            {
                _companySelectedRespository = companyRepo;

                IProcessCompanyBase processCompany = _requestBtwDomains
                    .RequestToProcess(companyRepo);

                await UpdateByProcessCompanyAsync(processCompany);

            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR => {Error}", ex.Message);
            }
        }
    }

    public async Task UpdateByProcessCompanyAsync(IProcessCompanyBase companyProcess)
    {
        try
        {
            List<IOffer> offers = await companyProcess.ProcessOffersCompanyAsync();

            List<IOfferEntity> offersProcesess = _mapperToOfferEntity.Convert(offers);

            IEnumerable<string> titlesInBd = (await _companySelectedRespository!.GetOffersAsync())
                .Select(offer => offer.Title);

            List<IOfferEntity> offersNews = offersProcesess
                .GroupBy(offer => offer.Title)
                .Select(group => group.First())
                .Where(offer => !titlesInBd.Contains(offer.Title))
                .Select(offer => offer)
                .ToList();

            await _companySelectedRespository!.InsertOffersAsync(offersNews);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}