using WonderfullOffers.Domain.Models.Domain.Models.OfferWeb;

namespace WonderfullOffers.Tests.UnitTest.Domain.PageProcess.AmazonPageProcess.Expected;

public static class OfferExpected
{
    public static List<AmazonOffer> GetOffersExpected()
    => new()
        {
            new()
            {
                Uri = new Uri("https://www.amazon.es/Enabot-EBO-Aplicaci%C3%B3n-Vigilancia-Bidireccional/dp/B0C4P1MF8V?pf_rd_r=5XC2YXE4T5XPECXV1PWV&pf_rd_t=Events&pf_rd_i=deals&pf_rd_p=9406c473-d65f-4512-bc20-ca3dc2632f3d&pf_rd_s=slot-14&ref=dlx_deals_gd_dcl_tlt_6_91a71f0f_dt_sl14_3d"),
                Title = "Enabot EBO SE Cámara para Perros con Aplicación,1080P Cámara para Mascotas,Cámara de Vigilancia Interior con Audio Bidireccional,Visión Nocturna,Detección de Movimiento,Modo de Privacidad",
                Disccount = 30,
                Img = new Uri("https://m.media-amazon.com/images/I/41VxHBaTZUL._AC_UF226,226_FMjpg_.jpg"),
                PriceWithinDisccount = 83.30M,
                PriceWithoutDisccount = 119
            },
            new()
            {
                Uri = new Uri("https://www.amazon.es/Escritorio-computadora-ergon%C3%B3mica-Reposacabeza-Ajustable/dp/B07R8K4RV7?pf_rd_r=4RWQ5J2EQ98JN7E2S8QJ&pf_rd_t=Events&pf_rd_i=deals&pf_rd_p=9406c473-d65f-4512-bc20-ca3dc2632f3d&pf_rd_s=slot-14&ref=dlx_deals_gd_dcl_img_1_4ba27790_dt_sl14_3d"),
                Title = "Soontrans Silla Gaming con Masajeador, Silla Gamer con Reposapiés Reposacabeza, Ergonómica, Cojín Lumbar de Masaje, Livesteam Youtube Playstation (Azul)",
                Disccount = 20,
                Img = new Uri("https://m.media-amazon.com/images/I/41gm6KIE32L._AC_UF226,226_FMjpg_.jpg"),
                PriceWithinDisccount = 98.59M,
                PriceWithoutDisccount = 115.99M
            },
        };
}
