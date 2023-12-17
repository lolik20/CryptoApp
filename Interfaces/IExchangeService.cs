namespace CryptoCalculator.Interfaces
{
    public interface IExchangeService
    {
        Task<decimal> GetRate(int fromId, decimal fromAmount, int toId);
    }
}
