using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.Models;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using CryptoExchange.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class BalanceQuery : IRequestHandler<BalanceRequest, decimal>
    {
        private readonly ApplicationContext _context;
        public BalanceQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<decimal> Handle(BalanceRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found by id");
            }
            decimal balance = await _context.Payments.Include(x => x.PaymentData).Where(x=>x.PaymentStatus==PaymentStatus.Succesful).Where(x => x.UserId == user.Id).SumAsync(x => x.Amount);
            return balance ;
        }
    }
}
