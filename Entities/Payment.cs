using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid MerchantId { get; set; }
        public User? User { get; set; }
        public decimal Amount { get; set; }
        [ForeignKey(nameof(Network))]
        public int? NetworkId { get; set; }
        public Network? Network { get; set; }
        [ForeignKey(nameof(Currency))]
        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public string? Title { get; set; }
        public DateTime Created { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentData? PaymentData { get; set; }

    }
}
