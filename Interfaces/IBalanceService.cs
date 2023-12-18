using CryptoExchange.Models;

namespace CryptoExchange.Interfaces
{
    public interface IBalanceService
    {
        Task<List<UserBalanceResponse>> GetBalance(Guid userId, bool isZeroBalances);
        Task<decimal> TopUp(Guid userId, int currencyId, decimal amount, bool isSave = true);
        Task<decimal> Withdraw(Guid userId, int currencyId, decimal amount, bool isSave = true);
        Task Convert(Guid userId, int fromId, int toId, decimal fromAmount, decimal commission);
    }
}
