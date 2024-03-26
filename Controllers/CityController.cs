using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{
    public class CityController :V1ControllerBase
    {
        public IMediator _mediator;
        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<CityResponse>>> GetCities([FromQuery]GetCitiesRequest request)
        {
            var cities = await _mediator.Send(request);
            return Ok(cities);
        }
    }
}
