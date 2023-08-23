using Microsoft.Extensions.Options;
using System.Diagnostics;
using WonderfullOffer.Api.Models.Settings.ErrorSettings;
using WonderfullOffers.Domain.Contracts.Domain.Processors;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Contracts.Domain.RequestBtwDomains;
using WonderfullOffers.Domain.Domain.CustomException;
using WonderfullOffers.Infraestructure.Contracts.Functionalities;
using WonderfullOffers.Infraestructure.Repositories;

namespace WonderfullOffers.Domain.Domain.RequestDomains;

public class RequestBtwDomains : IRequestBtwDomains
{
    private readonly IAmazonProcess _amazonProcess;
    private readonly IDouglasPageProcess _douglasPageProcess;
    private readonly ErrorSettings _errorSettings;

    public RequestBtwDomains(
        IAmazonProcess amazonProcess,
        IOptions<ErrorSettings> optionError
    )
    {
        _amazonProcess = amazonProcess;
        _errorSettings = optionError.Value;
    }

    public IProcessCompanyBase RequestToProcess(IGenericRepository repository)
    {
        string nameRepo = repository.GetType().Name;
        switch (nameRepo)
        {
            case nameof(AmazonRepository):
                return _amazonProcess;

            default:
                throw new ArgumentException(string.Format(
                        _errorSettings.RepositoryNotFound,
                        StackTree.GetPathError(new StackTrace(true)),
                        nameRepo
                    )
                );
                
        }
    }
}
