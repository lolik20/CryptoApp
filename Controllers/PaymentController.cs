using CryptoExchange.Attributes;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CryptoExchange.Controllers
{
    public class PaymentController : V1ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [IpAuthorization]
        [HttpGet("url")]
        public async Task<ActionResult<string>> GetLink([FromQuery] CreatePaymentRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.isSuccessful)
            {
               
                return Ok(response.message);

            }
            return BadRequest(response.message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPaymentResponse>> GetData([FromRoute] GetUserPaymentRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut()]
        public async Task<ActionResult<UpdatePaymentResponse>> Update([FromBody] UpdatePaymentRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
      

    }
}
