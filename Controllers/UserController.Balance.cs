using CryptoExchange.Entities;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{


    public partial class UserController
    {
        [HttpGet("/balance")]
        public async Task<ActionResult<List<BalanceResponse>>> GetBalance([FromRoute] BalanceRequest request, [FromQuery] bool isZeroBalances)
        {
            var balance = await _mediator.Send(request);

            return Ok(balance);

        }
        

    }
}
