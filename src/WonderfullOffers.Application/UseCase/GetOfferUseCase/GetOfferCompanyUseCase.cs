using Microsoft.Extensions.Options;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Application.Contracts.UseCase.GetOfferUseCase;
using WonderfullOffers.Application.Models.Enums;
using WonderfullOffers.Domain.Contracts.Mappers;
using WonderfullOffers.Domain.Domain.CustomException;
using WonderfullOffers.Infraestructure.Contracts.Models;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Application.UseCase.GetOfferUseCase;

public class GetOfferCompanyUseCase : IGetOfferCompanyUseCase
{
    private readonly IAmazonRepository _amazonRepository;
    private readonly IDouglasRepository _douglasRepository;
    private readonly IDruniRepository _druniRepository;
    private readonly IMaquillaliaRepository _maquillaliaRepository;
    private readonly IPrimorRepository _primorRepository;
    private readonly ISheinRepository _sheinRepository;
    private readonly ISpringfieldRepository _springfieldRepository;
    private readonly IStradivariusRepository _stradivariusRepository;
    private readonly IWomensecretRepository _womensecretRepository;
    private readonly IZalandoRepository _zalandoRepository;
    private readonly IConvertToOfferEntity _mapperToOfferEntity;
    private readonly ErrorSettings _errorSettings;

    public GetOfferCompanyUseCase(
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
        IConvertToOfferEntity mapperToOfferEntity,
        IOptions<ErrorSettings> optionError)
    {
        _amazonRepository = amazonRepository;
        _douglasRepository = douglasRepository;
        _druniRepository = druniRepository;
        _maquillaliaRepository = maquillaliaRepository;
        _primorRepository = primorRepository;
        _sheinRepository = sheinRepository;
        _springfieldRepository = springfieldRepository;
        _stradivariusRepository = stradivariusRepository;
        _womensecretRepository = womensecretRepository;
        _zalandoRepository = zalandoRepository;
        _errorSettings = optionError.Value;
        _mapperToOfferEntity = mapperToOfferEntity;
    }
    public async Task<int> GetNumberOffersCompanyAsync(string type)
    {
        Company enumInfo = Enum.Parse<Company>(type, ignoreCase: true);
        switch (enumInfo)
        {
            case Company.Amazon:
                return await _amazonRepository.GetNumberOffersAsync();

            case Company.Douglas:
                return await _douglasRepository.GetNumberOffersAsync();

            case Company.Druni:
                return await _druniRepository.GetNumberOffersAsync();

            case Company.Maquillalia:
                return await _maquillaliaRepository.GetNumberOffersAsync();

            case Company.Primor:
                return await _primorRepository.GetNumberOffersAsync();

            case Company.Shein:
                return await _sheinRepository.GetNumberOffersAsync();

            case Company.Springfield:
                return await _springfieldRepository.GetNumberOffersAsync();

            case Company.Stradivarius:
                return await _stradivariusRepository.GetNumberOffersAsync();

            case Company.Women:
                return await _womensecretRepository.GetNumberOffersAsync();

            case Company.Zalanado:
                return await _zalandoRepository.GetNumberOffersAsync();

            default:
                throw new ArgumentException(
                    string.Format(
                    _errorSettings.ParseOfferNotFound,
                    StackTree.GetPathError(new StackTrace(true)),
                    enumInfo.ToString()
                    )
                );
        }
    }

    public async Task<List<IOfferEntity>> GetOffersCompanyAsync(
        string type,
        int paginationFrontEnd)
    {
        Company enumInfo = Enum.Parse<Company>(type, ignoreCase: true);
        switch (enumInfo)
        {
            case Company.Amazon:
                return await _amazonRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Douglas:
                return await _douglasRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Druni:
                return await _druniRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Maquillalia:
                return await _maquillaliaRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Primor:
                return await _primorRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Shein:
                return await _sheinRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Springfield:
                return await _springfieldRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Stradivarius:
                return await _stradivariusRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Women:
                return await _womensecretRepository.GetOffersAsync(paginationFrontEnd);

            case Company.Zalanado:
                return await _zalandoRepository.GetOffersAsync(paginationFrontEnd);

            default:
                throw new ArgumentException(
                    string.Format(
                    _errorSettings.ParseOfferNotFound,
                    StackTree.GetPathError(new StackTrace(true)),
                    enumInfo.ToString()
                    )
                );
        }
    }

}