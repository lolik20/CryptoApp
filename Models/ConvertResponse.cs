using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models
{
    public class ConvertResponse : ConvertRequest
    {
        public decimal Rate { get; set; }
        public decimal ToAmount { get; set; }
    }
}
