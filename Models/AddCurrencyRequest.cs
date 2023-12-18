using CryptoExchange.Entities;

namespace CryptoExchange.Models
{
    public class AddCurrencyRequest
    {
        public CurrencyType Type { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
    }
}
