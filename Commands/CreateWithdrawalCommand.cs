using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TradePack.Interfaces;

namespace CryptoExchange.Commands
{
    public class CreateWithdrawalCommand : IRequestHandler<CreateWithdrawalRequest, CreateWithdrawalResponse>
    {
        private readonly ApplicationContext _context;
        private readonly IByBitService _byBitService;
        public CreateWithdrawalCommand(ApplicationContext context, IByBitService byBitService)
        {
            _context = context;
            _byBitService = byBitService;
        }
        public async Task<CreateWithdrawalResponse> Handle(CreateWithdrawalRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            if(request.Amount < 1000m)
            {
                throw new ValidationException("Amount less than 1000");
            }
            using (var transaction = _context.Database.BeginTransaction(isolationLevel:System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    var withdrawal = new Withdrawal
                    {
                        Amount = request.Amount,
                        WithdrawalType = request.WithdrawalType,
                        Telegram = request.Telegram,
                        WhatsApp = request.WhatsApp,

                    };
                    switch (request.WithdrawalType)
                    {
                        case WithdrawalType.Cash:
                            if (request.DeliveryData == null)
                            {
                                throw new ValidationException("No delivery data");
                            }
                            var deliveryData = request.DeliveryData;

                            withdrawal.DeliveryData = new WithdrawalCash
                            {
                                Address = deliveryData.Address,
                                Phone = deliveryData.Phone,
                                GoogleMapsUrl = deliveryData.GoogleMapsUrl,
                                CityId = deliveryData.CityId,
                                CurrencyId = deliveryData.CurrencyId

                            };
                            var rates = await _byBitService.GetRate(100_000, "581", TradePack.Entities.OperationType.Sell, "RUB");
                            var rate = rates.First();
                            withdrawal.Rate = rate * 0.995m;
                            break;
                    }
                    await _context.Withdrawals.AddAsync(withdrawal);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
            return new CreateWithdrawalResponse(true, "Withdrawal request created");
        }
    }
}
