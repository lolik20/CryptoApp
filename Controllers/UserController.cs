using CryptoExchange.Entities;
using CryptoExchange.Interfaces;
using CryptoExchange.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{
    public partial class UserController : V1ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UserController( IUserService userService,IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }
        [HttpPost]
        public ActionResult<Guid> AddUser([FromBody] AddUserRequest request)
        {

            var userId = _userService.AddUser(request);
            return Ok(userId);


        }





    }
}
