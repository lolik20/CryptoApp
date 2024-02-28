using CryptoExchange.Entities;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.ResponseModels
{
    public class CurrencyResponse
    {
        public int Id { get; set; }
        public CurrencyType Type { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string Code { get; set; }
        public string? ContractAddress { get; set; }
    }
}
