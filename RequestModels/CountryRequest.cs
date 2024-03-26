using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class CountryRequest :IRequest<List<CountryResponse>>
    {
    }
}
