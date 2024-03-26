using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public CurrencyType Type { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string? ImageUrl { get; set; }
        public Bank? Bank { get; set; }  
        public List<CurrencyNetwork>? Networks { get; set; }
        public List<PaymentData>? PaymentDatas { get; set; }
    }
}
