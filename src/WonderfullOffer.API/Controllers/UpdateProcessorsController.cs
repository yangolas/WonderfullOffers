using Microsoft.AspNetCore.Mvc;
using WonderfullOffers.Domain.Contracts.Domain.UpdateOffers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
