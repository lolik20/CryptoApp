using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.RequestModels
{
    public class UserRequest :IRequest<UserResponse>
    {
        public Guid Id { get; set; }
    }
}
