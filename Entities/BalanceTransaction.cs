using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class BalanceTransaction
    {

        public Guid Id { get; set; }
        public OperationType OperationType { get; set; }
        public decimal Amount { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey(nameof(Currency))]
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public DateTime Created { get; set; }
    }
}
