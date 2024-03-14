using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class GetMerchantPaymentsRequest:IRequest<List<MerchantPaymentResponse>>
    {
        public Guid MerchantId { get; set; }
    }
}
