using FluentAssertions;
using Microsoft.Extensions.Options;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings;
using WonderfullOffer.Api.Models.Settings.PageProcessSettings.Amazon;
using WonderfullOffers.Domain.Models.Domain.Models.PageWeb;
using WonderfullOffers.Tests.UnitTest.Hosting;

namespace WonderfullOffers.Tests.UnitTest.Domain.PageWeb;

public class GetPageWebServiceTest
{
    private readonly Host _host;
    private readonly AmazonSettings _amazonSettings;
    private readonly DouglasPageProcessSettings _douglasSettings;
    private readonly DruniPageProcessSettings _druniSettings;
    private readonly MaquillaliaPageProcessSettings _maquillaliaSettings;
    private readonly PrimorPageProcessSettings _primorSettings;
    private readonly SheinPageProcessSettings _sheinSettings;
    private readonly SpringfieldPageProcessSettings _springfieldSettings;
    private readonly StradivariusPageProcessSettings _stradivariusSettings;
    private readonly WomensecretPageProcessSettings _womensecretSettings;
    private readonly ZalandoPageProcessSettings _zalandoSettings;

    private const string MessagePageNotFound = $"\nBecause Page Not Found 404:\nCheck if the page exists else there are something wrong in code";


    public GetPageWebServiceTest()
    {
        _host = new();
        _amazonSettings = _host.GetService<IOptions<AmazonSettings>>().Value;
        _douglasSettings = _host.GetService<IOptions<DouglasPageProcessSettings>>().Value;
        _druniSettings = _host.GetService<IOptions<DruniPageProcessSettings>>().Value;
        _maquillaliaSettings = _host.GetService<IOptions<MaquillaliaPageProcessSettings>>().Value;
        _primorSettings = _host.GetService<IOptions<PrimorPageProcessSettings>>().Value;
        _sheinSettings = _host.GetService<IOptions<SheinPageProcessSettings>>().Value;
        _springfieldSettings = _host.GetService<IOptions<SpringfieldPageProcessSettings>>().Value;
        _stradivariusSettings = _host.GetService<IOptions<StradivariusPageProcessSettings>>().Value;
        _womensecretSettings = _host.GetService<IOptions<WomensecretPageProcessSettings>>().Value;
        _zalandoSettings = _host.GetService<IOptions<ZalandoPageProcessSettings>>().Value;
    }

    //[Fact]
    //public async Task Should_Get_Content_Amazon_Pageweb()
    //{
    //    Page expected = await _sut.GetPageWebAsync(new Uri(_amazonSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    //[Fact]
    //public async Task Should_Get_Content_Douglas_Pageweb()
    //{
    //    Page expected = await _sut.GetPageWebAsync(new Uri(_douglasSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    //[Fact]
    //public async Task Should_Get_Content_Druni_Pageweb()
    //{
    //    Page expected = await _sut.GetPageWebAsync(new Uri(_druniSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    //[Fact]
    //public async Task Should_Get_Content_Maquillalia_Pageweb()
    //{
    //    Page expected = await _sut.GetPageWebAsync(new Uri(_maquillaliaSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    //[Fact]
    //public async Task Should_Get_Content_Primor_Pageweb()
    //{

    //    Page expected = await _sut.GetPageWebAsync(new Uri(_primorSettings.MainDomainUri));

    //    expected.Should().NotBeNull();
    //}

    //[Fact]
    //public async Task Should_Get_Content_Shein_Pageweb()
    //{

    //    Page expected = await _sut.GetPageWebAsync(new Uri(_sheinSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    //[Fact]
    //public async Task Should_Get_Content_Springfield_Pageweb()
    //{

    //    Page expected = await _sut.GetPageWebAsync(new Uri(_springfieldSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    //[Fact]
    //public async Task Should_Get_Content_Stradivarius_Pageweb()
    //{

    //    Page expected = await _sut.GetPageWebAsync(new Uri(_stradivariusSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    //[Fact]
    //public async Task Should_Get_Content_Womensecret_Pageweb()
    //{

    //    Page expected = await _sut.GetPageWebAsync(new Uri(_womensecretSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    //[Fact]
    //public async Task Should_Get_Content_Zalando_Pageweb()
    //{
    //    Page expected = await _sut.GetPageWebAsync(new Uri(_zalandoSettings.MainDomainUri));

    //    expected.Should().NotBeNull(MessagePageNotFound);
    //}

    [Theory]
    [InlineData("https://www.amazon.es/gp/goldbox/?ie=UTF8&amp;ref_=sv__2&amp;deals-widget=%257B%2522version%2522%253A1%252C%2522viewIndex%2522%253A0%252C%2522presetId%2522%253A%2522deals-collection-lightning-deals%2522%252C%2522dealType%2522%253A%2522LIGHTNING_DEAL%2522%252C%2522sorting%2522%253A%2522BY_SCORE%2522%257D&amp;pd_rd_w=ch5wn&amp;content-id=amzn1.sym.7d79bf2b-4e72-4144-a0f7-58bd209cc2de&amp;pf_rd_p=7d79bf2b-4e72-4144-a0f7-58bd209cc2de&amp;pf_rd_r=BTEKT1YG2D6X4TDBFZ6V&amp;pd_rd_wg=xwKRo&amp;pd_rd_r=325152c9-441a-4e83-a309-ffc302be0749")]
    public async Task Should_Get_Content_Amazon_Redirect_Pageweb(string uri)
    {
        List<Uri> uris = new() { new Uri(uri) };



    }

    [Theory]
    [InlineData("https://www.amazon.es/Potensic-Transmisi%C3%B3n-Tecnolog%C3%ADa-ShakeVanish-Auto-Retorno/dp/B0BKL638FZ?pf_rd_r=4RWQ5J2EQ98JN7E2S8QJ&pf_rd_t=Events&pf_rd_i=deals&pf_rd_p=9406c473-d65f-4512-bc20-ca3dc2632f3d&pf_rd_s=slot-14&ref=dlx_deals_gd_dcl_tlt_0_2f357af0_dt_sl14_3d")]
    public async Task Should_Get_Content_Amazon_Redirect_Pageweb2(string uri)
    {
        List<Uri> uris = new() { new Uri(uri) };


    }

    [Theory]
    [InlineData("https://www.amazon.es/Escritorio-computadora-ergon%C3%B3mica-Reposacabeza-Ajustable/dp/B07R8K4RV7?pf_rd_r=4RWQ5J2EQ98JN7E2S8QJ&pf_rd_t=Events&pf_rd_i=deals&pf_rd_p=9406c473-d65f-4512-bc20-ca3dc2632f3d&pf_rd_s=slot-14&ref=dlx_deals_gd_dcl_img_1_4ba27790_dt_sl14_3d")]
    public async Task Should_Get_Content_Amazon_Redirect_Pageweb3(string uri)
    {
        List<Uri> uris = new() { new Uri(uri) };

    }
}
