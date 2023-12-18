using CryptoExchange.Entities;
using CryptoExchange.Interfaces;
using CryptoExchange.Services;
using CryptoExchange.Interfaces;
using CryptoExchange.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static CryptoExchange.Services.BalanceService;

namespace CryptoExchange.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IBalanceService _balanceService;
        private readonly IUserService _userService;
        public UserController(IBalanceService balanceService, IUserService userService)
        {
            _balanceService = balanceService;
            _userService = userService;
        }
        [HttpPost]
        public ActionResult<Guid> AddUser([FromBody] AddUserRequest request)
        {
            try
            {
                var userId = _userService.AddUser(request);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{userId}/balance")]
        public async Task<ActionResult<List<UserBalanceResponse>>> GetBalance([FromRoute] Guid userId, [FromQuery] bool isZeroBalances)
        {
            try
            {
                var balance =await _balanceService.GetBalance(userId, isZeroBalances);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}/balance/operation")]
        public async Task< ActionResult<string>> Operaton([FromRoute] Guid userId, [FromBody] BalanceOperationRequest request)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
