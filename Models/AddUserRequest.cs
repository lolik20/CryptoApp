using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models
{
    public class AddUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Url]
        public string WebsiteUrl { get; set; }
    }
}
