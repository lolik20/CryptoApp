using CryptoExchange.ResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.RequestModels
{
    public class CreatePaymentRequest:IRequest<CreatePaymentResponse>
    {
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
        public string CurrencyId { get; set; }

        public string? Title { get; set; }
    }
}
