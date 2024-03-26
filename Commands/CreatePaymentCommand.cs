using Bybit.Net.Clients;
using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nethereum.Web3;

namespace CryptoExchange.Commands
{
    public class CreatePaymentCommand : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
    {
        private readonly ApplicationContext _context;
        public CreatePaymentCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            var merchant = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (merchant == null)
            {
                return new CreatePaymentResponse(false, "Merchant not found");
            }
            var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == request.CurrencyId.ToLower().Trim());
            if (currency == null)
            {
                throw new NotFoundException($"Currency {request.CurrencyId} not found");
            }

            var payment = await _context.Payments.AddAsync(new Payment
            {
                Amount = request.Amount,
                CurrencyId = currency.Id,
                UserId = request.UserId,
                Title = request.Title,
            });
            
            await _context.SaveChangesAsync();
            return new CreatePaymentResponse(true, payment.Entity.Id.ToString());
        }
    }
}
