namespace CryptoExchange.Models
{
    public class ConvertResponse : ConvertRequest
    {
        public required decimal Rate { get; set; }
        public required decimal ToAmount { get; set; }
    }
}
