using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class UpdatePaymentRequest:IRequest<UpdatePaymentResponse>
    {
        public Guid Id { get; set; }
        public int CurrencyId { get; set; }
        public int NetworkId { get; set; }
    }
}
