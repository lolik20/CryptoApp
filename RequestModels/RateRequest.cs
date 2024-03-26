using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class RateRequest : IRequest<RateResponse>
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}
