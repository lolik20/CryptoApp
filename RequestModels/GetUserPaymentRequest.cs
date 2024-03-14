using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class GetUserPaymentRequest:IRequest<UserPaymentResponse>
    {
        public Guid Id { get; set; }
    }
}
