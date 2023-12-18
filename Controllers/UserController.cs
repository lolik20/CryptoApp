using CryptoExchange.Entities;
using CryptoExchange.Interfaces;
using CryptoExchange.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UserController : ControllerBase
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

            var userId = _userService.AddUser(request);
            return Ok(userId);


        }





    }
}
