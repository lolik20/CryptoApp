using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Entities
{
    public class Network
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Symbol { get; set; }
        public List<CurrencyNetwork>? Currencies { get; set; }
        public string? ImageUrl { get; set; }
        public List<PaymentData>? PaymentDatas { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
