using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class GetPaymentRequest:IRequest<PaymentResponse>
    {
        public Guid Id { get; set; }
    }
}
