using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("link")]
        public async Task<ActionResult<string>> GetLink([FromQuery] CreatePaymentRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.isSuccessful)
            {
                return Ok(response.message);

            }
            return BadRequest(response.message);
        }
        [HttpGet("{id}/data")]
        public async Task<ActionResult<PaymentResponse>> GetData([FromRoute] GetPaymentRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        //[HttpGet("button")]
        //public async Task<ActionResult<string>> GetButton()
        //{
        //    string file = System.IO.File.ReadAllText("./button.html");
        //    return new ContentResult
        //    {
        //        Content = file,
        //        ContentType = "text/html"
        //    };
        //}
    }
}
