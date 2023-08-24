using Microsoft.AspNetCore.Mvc;
using WonderfullOffers.Domain.Contracts.Domain.UpdateOffers;

namespace WonderfullOffer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateProcessorsController : ControllerBase
    {
        private readonly IUpdateOfferService _updateOfferService;
        public UpdateProcessorsController(IUpdateOfferService updateOfferService)
        {
            _updateOfferService = updateOfferService;
        }
        
        [HttpGet]
        public async Task Get()
        {
            await _updateOfferService.RepositoriesToUpdateAsync();
        }
    }
}
