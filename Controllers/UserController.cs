using CryptoExchange.Entities;
using CryptoExchange.Interfaces;
using CryptoExchange.Models;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{
    public partial class UserController : V1ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UserController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser([FromRoute]Guid id)
        {
            var user =await _mediator.Send(new UserRequest { Id = id });
            
            return Ok(user);
        }
        [HttpPost]
        public ActionResult<Guid> AddUser([FromBody] AddUserRequest request)
        {

            var userId = _userService.AddUser(request);
            return Ok(userId);


        }
      



    }
}
