using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.Processors.FindProcessBase;
using WonderfullOffers.Domain.Models.Domain.Models.Contracts;
using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;
using WonderfullOffers.Infraestructure.Contracts.Repository;

namespace WonderfullOffers.Domain.Domain.Processors;

public class MaquillaliaPageProcess : IMaquillaliaPageProcess
{
    private readonly IExtractorAmazonProcess _extractorAmazonProcess;
    private readonly IFindProcess _findProcess;
    private readonly AmazonSettings _amazonSettings;

    public MaquillaliaPageProcess(
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

    public async Task<List<AmazonOffer>> ProcessOffersCompanyAsync()
    {
        List<AmazonOffer> offers = new();

        return offers;
    }

    Task<List<IOffer>> IProcessCompanyBase.ProcessOffersCompanyAsync()
    {
        throw new NotImplementedException();
    }
}
