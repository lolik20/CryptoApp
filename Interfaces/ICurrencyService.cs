namespace CryptoExchange.Interfaces
{
    public interface ICurrencyService
    {
        Task<decimal> GetRate(int fromId, decimal fromAmount, int toId);
        decimal CalculateAmountWithComission(decimal amount, decimal rate, decimal commission);
    }
}
