using CryptoExchange.Entities;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{
    public class CountryController : V1ControllerBase
    {
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<CountryResponse>>> GetAll([FromQuery] CountryRequest request)
        {
            var countries = await _mediator.Send(request);
            return Ok(countries);
        }
    }
}
