using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class Withdrawal
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        

        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public WithdrawalType WithdrawalType { get; set; }

        public WithdrawalStatus Status { get; set; }
        public WithdrawalCash? DeliveryData { get; set; }
        public string? Telegram { get; set; }
        public string? WhatsApp { get; set; }
        public User? User { get; set; }
        public DateTime Created { get; set; }

    }
}
