using CryptoExchange.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.ResponseModels
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        public decimal FromAmount { get; set; }
        public Currency FromCurrency { get; set; }
        public string? Title { get; set; }
        public decimal? ToAmount { get; set; }
        public Network? ToNetwork { get; set; }
        public Currency? ToCurrency { get; set; }
        public string? WalletAddress { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
