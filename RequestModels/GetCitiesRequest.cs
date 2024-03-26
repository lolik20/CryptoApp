using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class GetCitiesRequest:IRequest<List<CityResponse>>
    {

    }
}
