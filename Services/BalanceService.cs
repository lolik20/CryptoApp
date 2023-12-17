using CryptoCalculator.Entities;
using CryptoCalculator.Interfaces;
using CryptoExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoCalculator.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly ApplicationContext _context;

        public BalanceService(ApplicationContext context)
        {
            _context = context;
        }
        public void Convert(Guid userId, int fromId, int toId, decimal fromAmount, decimal toAmount)
        {
            Withdraw(userId, fromId, fromAmount, false);
            TopUp(userId, toId, toAmount, false);
            _context.SaveChanges();
        }
        public List<UserBalanceResponse> GetBalance(Guid userId, bool isZeroBalances)
        {
            var balances = _context.Balances.Where(x => x.UserId == userId).Include(x => x.Currency).Select(x => new UserBalanceResponse
            {
                Amount = x.Value,
                Currency = x.Currency.Name,
                CurrencyType = x.Currency.Type.ToString()

            }).ToList();
            if (isZeroBalances)
            {
                var currencies = _context.Currencies.Select(x => new UserBalanceResponse
                {
                    Currency = x.Name,
                    CurrencyType = x.Type.ToString(),
                    Amount = 0
                }).ToList();
                balances = balances.Union(currencies).DistinctBy(x => x.Currency).ToList();
            }

            return balances;
        }
        public decimal TopUp(Guid userId, int currencyId, decimal amount, bool isSave = true)
        {
            var balance = _context.Balances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == currencyId);

            if (balance == null)
            {
                var currency = _context.Currencies.FirstOrDefault(x => x.Id == currencyId);
                if (currency == null)
                {
                    throw new Exception($"Currency with id {currencyId} not exist");
                }
                _context.Balances.Add(new Entities.UserBalance { CurrencyId = currencyId, Value = amount, UserId = userId });
                _context.SaveChanges();
                return amount;
            }

            lock (balance)
            {
                if(balance.Value + amount > 1_000_000_000)
                balance.Value += amount;
                _context.BalanceTransactions.Add(new BalanceTransaction
                {
                    Amount = amount,
                    CurrencyId = currencyId,
                    OperationType = OperationType.TopUp,
                    UserId = userId,

                });
                if (isSave)
                {
                    _context.SaveChanges();
                }
                return balance.Value;
            }
        }
        public decimal Withdraw(Guid userId, int currencyId, decimal amount, bool isSave = true)
        {
            var balance = _context.Balances.FirstOrDefault(x => x.UserId == userId && x.CurrencyId == currencyId);
            if (balance == null)
            {
                throw new Exception("Insufficient balance");
            }
            lock (balance)
            {
                if (balance.Value - amount < 0m)
                {
                    throw new Exception("Insufficient balance");
                }
                balance.Value -= amount;
                _context.BalanceTransactions.Add(new BalanceTransaction
                {
                    Amount = amount,
                    CurrencyId = currencyId,
                    OperationType = OperationType.Withdraw,
                    UserId = userId,

                });
                if (balance.Value == 0m)
                {
                    _context.Balances.Remove(balance);
                }
                if (isSave)
                {
                    _context.SaveChanges();
                }
            }
            return balance.Value;

        }


    }
}
