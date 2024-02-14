using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class PaymentData
    {

        [Key]
        [ForeignKey(nameof(Payment))]
        public Guid PaymentId { get; set; }
        public Payment? Payment { get; set; }
        [ForeignKey(nameof(Network))]
        public int? NetworkId { get; set; }
        public Network? Network { get; set; }
        [ForeignKey(nameof(Currency))]
        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; } 
        public string? TxHash { get; set; }
        public string? PrivateKey { get; set; }
        public string? WalletAddress { get; set; }
        public decimal ToAmount { get; set; }
        
    }
}
