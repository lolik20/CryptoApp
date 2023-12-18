using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class UserBalance
    {
        [ForeignKey(nameof(Currency))]
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        [ForeignKey(nameof(User))]
        [Required]
        public  Guid UserId { get; set; }
        public User? User { get; set; }
        public decimal Value { get; set; }
    }
}
