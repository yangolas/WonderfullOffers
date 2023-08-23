using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.FindProcessBase;
using WonderfullOffers.Domain.Models.Domain.Models.Contracts;
using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;
using WonderfullOffers.Infraestructure.Contracts.Repository;

namespace WonderfullOffers.Domain.Domain.Processors;

public class StradivariusPageProcess : IStradivariusPageProcess
{
    private readonly IExtractorAmazonProcess _extractorAmazonProcess;
    private readonly IFindProcess _findProcess;
    private readonly AmazonSettings _amazonSettings;

    public StradivariusPageProcess(
        IOptions<AmazonSettings> options,
        IExtractorAmazonProcess extractorAmazonProcess,
        IFindProcess findProcess)
    {
        _amazonSettings = options.Value;
        _extractorAmazonProcess = extractorAmazonProcess;
        _findProcess = findProcess;
    }

    public bool IsThisProcess(Type webType)
    {
        return typeof(IAmazonRepository).Equals(webType);
    }

    public Task<List<AmazonOffer>> ProcessOffersCompanyAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<AmazonOffer>> ProcessOffersWebInPageCompanyAsync()
    {
        List<AmazonOffer> offers = new();
        return offers;
    }

    Task<List<IOffer>> IProcessCompanyBase.ProcessOffersCompanyAsync()
    {
        throw new NotImplementedException();
    }
}
