using static CryptoCalculator.Services.BalanceService;

namespace CryptoCalculator.Interfaces
{
    public interface IBalanceService
    {
        List<UserBalanceResponse> GetBalance(Guid userId,bool isZeroBalances);
        decimal TopUp(Guid userId, int currencyId, decimal amount, bool isSave = true);
        decimal Withdraw(Guid userId, int currencyId, decimal amount, bool isSave = true);
        void Convert(Guid userId, int fromId, int toId, decimal fromAmount, decimal toAmount);
    }
}
