namespace CryptoCalculator.Interfaces
{
    public interface IXeService
    {
        Task<decimal> GetRate(string from, string to);
    }
}
