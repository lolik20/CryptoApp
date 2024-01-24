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
    }
}
