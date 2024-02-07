using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Commands
{
    public class WithdrawCommand : IRequestHandler<WithdrawRequest, WithdrawResponse>
    {
        private readonly ApplicationContext _context;
        private readonly ILogger _logger;
        public WithdrawCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<WithdrawResponse> Handle(WithdrawRequest request, CancellationToken cancellationToken)
        {
            var currency = _context.Currencies.FirstOrDefault(x => x.Id == request.CurrencyId);
            if (currency == null)
            {
                throw new NotFoundException($"Currency with id {request.CurrencyId} not exist");
            }
            var balance = _context.Balances.FirstOrDefault(x => x.UserId == request.UserId && x.CurrencyId == request.CurrencyId);
            if (balance == null)
            {
                throw new InsufficientBalanceException("Insufficient balance");
            }

            if (balance.Value - request.Amount < 0m)
            {
                throw new InsufficientBalanceException("Insufficient balance");
            }
            using (var transaction = await _context.Database.BeginTransactionAsync(isolationLevel: System.Data.IsolationLevel.Serializable, cancellationToken))
            {
                try
                {
                    balance.Value -= request.Amount;
                    await _context.BalanceTransactions.AddAsync(new BalanceTransaction
                    {
                        Amount = request.Amount,
                        CurrencyId = request.CurrencyId,
                        OperationType = OperationType.Withdraw,
                        UserId = request.UserId,

                    });
                    if (balance.Value == 0m)
                    {
                        _context.Balances.Remove(balance);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                }

                catch (TaskCanceledException ex)
                {
                    throw new BalanceOperationException("Withdraw canceled");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error while withdraw for userId: {0}", balance.UserId);
                    throw new BalanceOperationException("Withdraw error");

                }
            }

            return new WithdrawResponse(true, "Баланс успешно пополнен");
        }
    }
}
