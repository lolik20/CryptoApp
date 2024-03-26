using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class Bank
    {
        [Key]
        [ForeignKey(nameof(Currency))]
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public string Name { get; set; }
        public int ByBitId { get; set; }
    }
}
