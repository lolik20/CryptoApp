using Bybit.Net.Clients;
using Bybit.Net.Interfaces.Clients;
using CryptoExchange.Entities;
using CryptoExchange.Interfaces;
using CryptoExchange.Models;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using Isopoh.Cryptography.Argon2;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Controllers
{

    public class CurrencyController : V1ControllerBase
    {
        private readonly IMediator _mediator;
        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator; 
        }


        [HttpGet("all")]
        public async Task<ActionResult<List<CurrencyResponse>>> GetAll([FromQuery] CurrencyRequest request)
        {

            var currencies = await _mediator.Send(request);
            if (currencies.Any())
            {
                return Ok(currencies);

            }
            return NotFound();

        }

    }
}
