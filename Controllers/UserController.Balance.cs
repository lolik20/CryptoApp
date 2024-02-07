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

            var balance = await _mediator.Send(new BalanceRequest { UserId = id, isZeroBalances = isZeroBalances });

            return Ok(balance);

        }
        [HttpPost("{id}/balance/convert")]
        public async Task<IActionResult> Convert([FromBody]ConvertRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(request, cancellationToken);
            if (response.isSuccessful)
            {
                return Ok(response.message);
            }
            return BadRequest(response.message);

        }
        
        //[HttpPost("{id}/balance/topup")]
        //public async Task<ActionResult> TopUp([FromRoute] Guid id, [FromQuery] decimal amount, [FromQuery] int currencyId)
        //{

        //    var topUpResponse = await _mediator.Send(new TopUpRequest(id, amount, currencyId));
        //    if (topUpResponse.isSuccessful)
        //    {
        //        return Ok(topUpResponse.message);

        //    }
        //    return BadRequest(topUpResponse.message);
        //}

        //[HttpPost("{id}/balance/withdraw")]
        //public async Task<ActionResult> Withdraw([FromRoute] Guid id, [FromQuery] decimal amount, [FromQuery] int currencyId)
        //{
        //    var withdrawResponse = await _mediator.Send(new WithdrawRequest(id, amount, currencyId));
        //    if (withdrawResponse.isSuccessful)
        //    {
        //        return Ok(withdrawResponse.message);

        //    }
        //    return BadRequest(withdrawResponse.message);
        //}

    }
}
