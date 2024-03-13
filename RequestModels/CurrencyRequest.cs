using CryptoExchange.Entities;
using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class CurrencyRequest :IRequest<List<CurrencyResponse>>
    {
        public CurrencyType[]? currencyTypes { get; set; }
    }
}
