using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Entities
{
    public class WithdrawalCrypto
    {
        [Key]
        [ForeignKey(nameof(Payment))]
        public Guid PaymentId { get; set; }
        public Payment? Payment { get; set; }
        [Required]
        public string WalletAddress { get; set; }
        [ForeignKey(nameof(Network))]
        public int NetworkId { get; set; }
        public Network? Network { get; set; }
    }
}
