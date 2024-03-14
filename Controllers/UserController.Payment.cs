using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Controllers
{
    public partial class UserController
    {
        [HttpGet("/payments")]
        public async Task<List<MerchantPaymentResponse>> GetPayments([FromQuery] GetMerchantPaymentsRequest request)
        {
            var payments = await _mediator.Send(request);

            return payments;
        }
    }
}
