using CryptoExchange.Entities;
using CryptoExchange.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Controllers
{
    public class NetworkController : V1ControllerBase
    {
        private readonly IMediator _mediator;
        public NetworkController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<Network>>> GetAll([FromQuery] NetworkRequest request)
        {
            var networks = await _mediator.Send(request);

            return Ok(networks);


        }
    }
}
