using CryptoExchange.Entities;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.ResponseModels
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WebsiteUrl { get; set; }

    }
}
