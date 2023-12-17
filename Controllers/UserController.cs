using CryptoCalculator.Entities;
using CryptoCalculator.Interfaces;
using CryptoCalculator.Services;
using CryptoExchange.Interfaces;
using CryptoExchange.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static CryptoCalculator.Services.BalanceService;

namespace CryptoCalculator.Controllers
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
                var balance = _balanceService.GetBalance(userId, isZeroBalances);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}/balance/operation")]
        public ActionResult Operaton([FromRoute] Guid userId, [FromBody] BalanceOperationRequest request)
        {
            try
            {
                switch (request.Type)
                {
                    case OperationType.Withdraw:

                        return Ok(_balanceService.Withdraw(userId, request.CurrencyId, request.Amount, true));
                    case OperationType.TopUp:

                        return Ok(_balanceService.TopUp(userId, request.CurrencyId, request.Amount, true));
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
