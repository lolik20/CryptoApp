using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Commands
{
    public class TopUpCommand : IRequestHandler<TopUpRequest, TopUpResponse>
    {
        private readonly ApplicationContext _context;
        private ILogger<TopUpCommand> _logger;

        public TopUpCommand(ApplicationContext context, ILogger<TopUpCommand> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TopUpResponse> Handle(TopUpRequest request, CancellationToken cancellationToken)
        {
            var currency = _context.Currencies.FirstOrDefault(x => x.Id == request.CurrencyId);
            if (currency == null)
            {
                throw new NotFoundException($"Currency with id {request.UserId} not found");
            }
            using (var transaction = await _context.Database.BeginTransactionAsync(isolationLevel: System.Data.IsolationLevel.Serializable, cancellationToken))
            {
                var balance = _context.Balances.FirstOrDefault(x => x.UserId == request.UserId && x.CurrencyId == request.CurrencyId);

                if (balance == null)
                {
                    _context.Balances.Add(new UserBalance { CurrencyId = request.CurrencyId, Value = request.Amount, UserId = request.UserId });
                }


                try
                {
                    balance.Value += request.Amount;
                   
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (TaskCanceledException ex)
                {
                    throw new BalanceOperationException("TopUp canceled");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error while TopUp for userId: {0}", balance.UserId);
                    throw new BalanceOperationException("TopUp error");


                }
            }
            return new TopUpResponse(true,"Top up successful");
        }
    }
}
