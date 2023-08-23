using WonderfullOffers.Domain.Contracts.Domain.UpdateOffers;
using WonderfullOffers.Tests.UnitTest.Hosting;

namespace WonderfullOffers.Tests.UnitTest.Domain.LogicOffers;

public class UpdateOfferServiceTest
{
    private readonly Host _host;
    private readonly IUpdateOfferService _sut;
    public UpdateOfferServiceTest()
    {
        _host = new();
        _sut = _host.GetService<IUpdateOfferService>();
    }

    [Fact]
    public async Task Should_Be()
    {
        await _sut.RepositoriesToUpdateAsync();
    }
}
