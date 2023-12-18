namespace CryptoExchange.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public CurrencyType Type { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public List<UserBalance>? Balances { get; set; }
        public List<BalanceTransaction>? Transactions { get; set; }
    }
}
