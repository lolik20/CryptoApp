using CryptoExchange.Services;

namespace CryptoExchange.Interfaces
{
    public interface IEthService
    {
        public Task<Wallet> CreateWallet(string url);
    }
}
