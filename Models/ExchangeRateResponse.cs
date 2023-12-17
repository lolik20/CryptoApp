namespace CryptoExchange.Models
{
    public class ExchangeRateResponse : ExchangeRequest
    {
        public required decimal Rate { get; set; }
        public required decimal ToAmount { get; set; }
    }
}
