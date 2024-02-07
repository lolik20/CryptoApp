using CryptoExchange.ResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.RequestModels
{
    public class ConvertRequest : IRequest<ConvertResponse>
    {
        [Range(1, int.MaxValue)]
        public int FromId { get; set; }
        [Range(1, int.MaxValue)]
        public int ToId { get; set; }
        public Guid UserId { get; set; }
        [Range(0, 1_000_000_000, ErrorMessage = $"Amount required more 0 and less than 1.000.000.000")]
        public decimal FromAmount { get; set; }
        [Range(0, 1, ErrorMessage = "Commission between 0 and 1")]
        public decimal Commission { get; set; }
    }
}
