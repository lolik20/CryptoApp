namespace CryptoExchange.Models
{
    public class AddUserRequest
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}
