using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace CryptoCalculator.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User? User { get; set; }

    }
}
