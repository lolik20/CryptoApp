using CryptoExchange.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{
    public partial class UserController
    {
        [HttpPost("withdrawal")]
        public async Task<IActionResult> CreateWithdrawal([FromBody] CreateWithdrawalRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
