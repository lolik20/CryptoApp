using CryptoExchange.ResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.RequestModels
{
    public class CreatePaymentRequest:IRequest<CreatePaymentResponse>
    {
        public decimal Amount { get; set; }
        public Guid MerchantId { get; set; }
        public string CurrencyId { get; set; }

        public string? Title { get; set; }
        public bool IsRedirect { get; set; } = false;
    }
}
