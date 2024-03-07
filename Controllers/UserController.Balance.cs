using CryptoExchange.Entities;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{


    public partial class UserController
    {
        [HttpGet("{id}/balance")]
        public async Task<ActionResult<List<BalanceResponse>>> GetBalance([FromRoute] Guid id, [FromQuery] bool isZeroBalances)
        {

            var balance = await _mediator.Send(new BalanceRequest { UserId = id });

            return Ok(balance);

        }
        

    }
}
