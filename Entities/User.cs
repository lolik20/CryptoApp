using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public UserProfile? Profile { get; set; }

        public List<UserBalance>? Balances { get; set; }
        public List<BalanceTransaction>? Transactions { get; set; }
    }
}
