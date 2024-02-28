using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.ResponseModels
{
    public class NetworkResponse
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Symbol { get; set; }
        public string? ImageUrl { get; set; }
        public string Url { get; set; }
        public string? ExplorerUrl { get; set; }
        public int ChainId { get; set; }

    }
}
