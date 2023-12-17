using System.ComponentModel.DataAnnotations;

namespace CryptoCalculator.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }
        public UserProfile? Profile { get; set; }

        public List<UserBalance>? Balances { get; set; }
        public List<BalanceTransaction>? Transactions { get; set; }
    }
}
