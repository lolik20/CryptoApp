using CryptoExchange.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.ResponseModels
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        public decimal Amount { get; set; }
        public int? NetworkId { get; set; }
        public int? CurrencyId { get; set; }
        public string? Title { get; set; }
    }
}
