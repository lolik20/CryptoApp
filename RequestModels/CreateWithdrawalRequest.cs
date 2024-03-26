using CryptoExchange.Entities;
using CryptoExchange.ResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.RequestModels
{
    public class CreateWithdrawalRequest : IRequest<CreateWithdrawalResponse>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Telegram { get; set; }
        [Phone]
        public string? WhatsApp { get; set; }
        public WithdrawalType WithdrawalType { get; set; }
        public WithdrawalCashRequest? DeliveryData { get; set; }
        public WithdrawalCardRequest? CardData { get; set; }
        public WithdrawalCryptoRequest? WalletData { get; set; }
      
    }
    public class WithdrawalCardRequest
    {
        [CreditCard]
        public string CreditCard { get; set; }
    }
    public class WithdrawalCryptoRequest
    {
        public string WalletAddress { get; set; }
    }
    public class WithdrawalCashRequest
    {
        public int CityId { get; set; }
        [Required]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        public int CurrencyId { get; set; }

        [Url]
        public string? GoogleMapsUrl { get; set; }
    }
}
