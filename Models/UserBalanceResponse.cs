namespace CryptoExchange.Models
{
    public class UserBalanceResponse
    {
        public required string Currency { get; set; }
        public decimal Amount { get; set; }
        public required string CurrencyType { get; set; }
    }
}
