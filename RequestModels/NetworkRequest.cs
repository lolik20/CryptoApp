using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class NetworkRequest:IRequest<List<NetworkResponse>>
    {
    }
}
