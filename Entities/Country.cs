using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<City>? Cities { get; set;}
    }
}
