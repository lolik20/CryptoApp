using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string WebsiteUrl { get; set; }
        [Required]
        public string Ip { get;set; }
        public List<Payment>? Payments { get; set; }
        public DateTime Created { get; set; }
        public List<Withdrawal>? Withdrawals { get; set; }
    }
}
