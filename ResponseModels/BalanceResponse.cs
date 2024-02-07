using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.ResponseModels
{
    public class BalanceResponse
    {
        public int CurrencyId { get; set; }
        [Required]
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        [Required]
        public string CurrencyType { get; set; }
    }
}
