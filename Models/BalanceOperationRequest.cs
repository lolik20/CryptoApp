using CryptoCalculator.Entities;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models
{
    public class BalanceOperationRequest
    {

        public OperationType Type { get; set; }
        [Range(0, int.MaxValue)]
        public int CurrencyId { get; set; }
        [Range(0, 1_000_000_000, ErrorMessage = $"Amount required more 0 and less than 1.000.000.000")]
        public decimal Amount { get; set; }

    }
}
