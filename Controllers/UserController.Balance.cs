using CryptoExchange.Entities;
using CryptoExchange.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{


    public partial class UserController
    {
        [HttpGet("{userId}/balance")]
        public async Task<ActionResult<List<UserBalanceResponse>>> GetBalance([FromRoute] Guid userId, [FromQuery] bool isZeroBalances)
        {

            var balance = await _balanceService.GetBalance(userId, isZeroBalances);
            return Ok(balance);

        }
        [HttpPost("{userId}/balance/convert")]
        public async Task<IActionResult> Convert([FromBody] ConvertRequest request, [FromRoute] Guid userId)
        {


            await _balanceService.Convert(userId, request.FromId, request.ToId, request.FromAmount, request.Commission);
            return Ok($"Convert success");

        }
        [HttpPost("{userId}/balance/operation")]
        public async Task<ActionResult<string>> Operaton([FromRoute] Guid userId, [FromBody] BalanceOperationRequest request)
        {

            switch (request.Type)
            {
                case OperationType.Withdraw:
                    var withdrawResult = await _balanceService.Withdraw(userId, request.CurrencyId, request.Amount, true);
                    return Ok($"New balance is {withdrawResult}");
                case OperationType.TopUp:
                    var topUpResult = await _balanceService.Withdraw(userId, request.CurrencyId, request.Amount, true);
                    return Ok($"New balance is {topUpResult}");
            }
            return BadRequest("Invalid Operation type");

        }

    }
}
