﻿using CryptoExchange.Models;

namespace CryptoExchange.Interfaces
{
    public interface IBalanceService
    {
        Task<List<UserBalanceResponse>> GetBalance(Guid userId, bool addZeroBalances);
        Task<decimal> TopUp(Guid userId, int currencyId, decimal amount, CancellationToken cancellationToken, bool isSave = true);
        Task<decimal> Withdraw(Guid userId, int currencyId, decimal amount, CancellationToken cancellationToken, bool isSave = true);
        Task Convert(Guid userId, int fromId, int toId, decimal fromAmount, decimal commission, CancellationToken cancellationToken);
    }
}
