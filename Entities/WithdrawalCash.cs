using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class WithdrawalCash
    {
        [Key]
        [ForeignKey(nameof(Withdrawal))]
        public Guid WithdrawalId { get; set; }
        public Withdrawal? Withdrawal { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Url]
        public string? GoogleMapsUrl { get; set; }
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public City? City { get; set; }
        [ForeignKey(nameof(Currency))]
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        
    }
}
