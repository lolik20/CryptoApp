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
        [HttpGet("link")]
        public async Task<ActionResult<string>> GetLink([FromQuery] CreatePaymentRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.isSuccessful)
            {
                if (request.IsRedirect)
                {
                    return Redirect($"http://localhost:3000/payment/{response.message}");
                }
                return Ok(response.message);

            }
            return BadRequest(response.message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentResponse>> GetData([FromRoute] GetPaymentRequest request)
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
        [HttpGet("button/{merchantId}/{amount}/{currency}")]
        public ContentResult GetButton([FromRoute] Guid merchantId, [FromRoute] decimal amount, [FromRoute] string currency)
        {
            StringBuilder html = new StringBuilder(System.IO.File.ReadAllText("./button.html"));
            html.Replace("$merchantId", merchantId.ToString());
            html.Replace("$amount", amount.ToString());
            html.Replace("$currency", currency);
            return new ContentResult
            {
                Content = html.ToString(),
                ContentType = "text/html"
            };
        }

    }
}
