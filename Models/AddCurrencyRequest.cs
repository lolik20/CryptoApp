using CryptoExchange.Entities;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models
{
    public class AddCurrencyRequest
    {
        public CurrencyType Type { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
