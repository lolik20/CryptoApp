namespace CryptoExchange.Interfaces
{
    public interface IXeService
    {
        Task<decimal> GetRate(string from, string to);
    }
}
