using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WonderfullOffers.Application.Contracts.UseCase.GetOfferUseCase;
using WonderfullOffers.Infraestructure.Contracts.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WonderfullOffer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersCompanyController : ControllerBase
    {
        private readonly IGetOfferCompanyUseCase _getOfferCompanyUseCase;
        public OffersCompanyController(IGetOfferCompanyUseCase getOfferCompanyUseCase)
        {
            _getOfferCompanyUseCase = getOfferCompanyUseCase;
        }

        [HttpGet("numberOffers/type/{type}")]
        public async Task<IActionResult> GetNumberOffersAsync(string type)
        {
            int numberOffers = await _getOfferCompanyUseCase.GetNumberOffersCompanyAsync(type);

            return Ok(numberOffers);
        }

        [HttpGet("type/{type}/pagination/{paginationFrontEnd}")]
        public async Task<IActionResult> GetOffersAsync(string type, int paginationFrontEnd)
        {
            List<IOfferEntity> offersResult = await _getOfferCompanyUseCase.GetOffersCompanyAsync(
                type,
                paginationFrontEnd
            );

            string json = JsonConvert.SerializeObject(offersResult, Formatting.Indented);
            return Ok(new { offers = json });
        }

    }
}
