using CryptoExchange.Entities;

namespace CryptoExchange.ResponseModels
{
    public class MerchantPaymentResponse
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public decimal Amount { get; set; }
        public string? Title { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
