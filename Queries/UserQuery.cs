using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class UserQuery : IRequestHandler<UserRequest, UserResponse>
    {
        private readonly ApplicationContext _context;
        public UserQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<UserResponse> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                WebsiteUrl = user.WebsiteUrl
            };


        }
    }
}
