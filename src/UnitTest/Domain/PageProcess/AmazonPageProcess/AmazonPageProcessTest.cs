using FluentAssertions;
using WonderfullOffers.Domain.Contracts.Domain.Processors.Amazon;
using WonderfullOffers.Domain.Models.Domain.Models.Contracts;
using WonderfullOffers.Domain.Models.Domain.Models.PageWeb;
using WonderfullOffers.Infraestructure.Contracts.Repository;
using WonderfullOffers.Tests.UnitTest.Domain.PageProcess.AmazonPageProcess.Fake;
using WonderfullOffers.Tests.UnitTest.Hosting;

namespace WonderfullOffers.Tests.UnitTest.Domain.PageProcess.AmazonPageProcess;

public class AmazonPageProcessTest
{
    private readonly Host _host;
    private readonly IAmazonProcess _sut;


    public AmazonPageProcessTest()
    {
        _host = new();
        _sut = _host.GetService<IAmazonProcess>();
    }

    

    

    //[Fact]
    //public void Should_Get_The_AmazonPageProcessSettings_MainDomainUri_From_Appsettings()
    //{
    //    Uri expected = _sut.GetDomainUri();
    //    Uri result = new Uri(_host.GetService<IOptions<AmazonSettings>>().Value.MainDomainUri);
    //    expected.Should().BeEquivalentTo(result);
    //}
    /// <summary>
    /// This test we need to think in another way bcause the offers in the web are changing...
    /// </summary>

    //[Fact]
    //public async void Should_Add_Extra_Information_To_Currents_Offers()
    //{
    //    List<Offer> expected = OfferFake.GetOffersFake();
    //    await _sut.AddExtraAmazonInfoToOffersAsync(expected);
    //    List<Offer> result = OfferExpected.GetOffersExpected();
        
    //    expected.Should().BeEquivalentTo(result);
    //}

    [Fact]
    public async void Should_Get_All_Offers_In_Company()
    {
        Page expected = PageFake.GetFakePageCompany();
        List<IOffer> offers = await _sut.ProcessOffersCompanyAsync();

        offers.Count.Should().BeGreaterThan(0);
    }
}
