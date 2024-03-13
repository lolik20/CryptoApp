using CryptoExchange.Entities;
using CryptoExchange.Services;

namespace CryptoExchange.Interfaces
{
    public interface IEthService
    {
        Wallet CreateWallet(string url);
        Task<decimal> GetBalance(string networkUrl, string contractAddress, string walletAddress, CurrencyType currencyType);
    }
}
