using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Entities
{
    public class WithdrawalCard
    {
        [Key]
        [ForeignKey(nameof(Payment))]
        public Guid PaymentId { get; set; }
        public Payment? Payment { get; set; }
        [Required]
        public string Bank { get; set; }
        [Required]
        [CreditCard]
        public string CreditCard { get; set; }
        [Required]
        public string FIO { get; set; }

    }
}
