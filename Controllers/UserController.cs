using CryptoCalculator.Entities;
using CryptoCalculator.Interfaces;
using CryptoCalculator.Services;
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
        private readonly ApplicationContext _context;
        public UserController(IBalanceService balanceService, ApplicationContext context)
        {
            _balanceService = balanceService;
            _context = context;
        }
        [HttpPost]
        public ActionResult<Guid> AddUser([FromBody] AddUserRequest request)
        {
            try
            {
                var existUser = _context.Users.FirstOrDefault(x => x.Name == request.Name);
                if (existUser != null)
                {
                    return BadRequest($"User already exist with name {existUser.Name}");
                }
                var newUser = new User { Name = request.Name, PasswordHash = Argon2.Hash(request.Password) };
                var newUserEntity = _context.Users.Add(newUser).Entity;
                var newUserProfile = new UserProfile() { UserId = newUserEntity.Id };
                _context.Profiles.Add(newUserProfile);
                _context.SaveChanges();
                return Ok(newUserEntity.Id);
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
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
            catch(Exception ex)
            {
                return Forbid (ex.Message); 
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
                return Forbid(ex.Message);
            }
        }

       

        
    }
}
