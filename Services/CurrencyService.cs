using CryptoExchange.Entities;
using CryptoExchange.Interfaces;

namespace CryptoExchange.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IXeService _xeService;
        private readonly ApplicationContext _context;
        public CurrencyService(IXeService xeService, ApplicationContext context)
        {
            _xeService = xeService;
            _context = context;
        }
        public async Task<decimal> GetRate(int fromId, decimal fromAmount, int toId)
        {
            Currency? from = _context.Currencies.FirstOrDefault(x => x.Id == fromId);
            if (from == null)
            {
                throw new Exception("From currency not found");
            }
            Currency? to = _context.Currencies.FirstOrDefault(x => x.Id == toId);
            if (to == null)
            {
                throw new Exception("To currency not found");
            }
            decimal rate = await _xeService.GetRate(from.Code, to.Code);
            if (rate == 0)
            {
                throw new Exception("Error calculating rate");
            }
            return rate;
        }
        public decimal CalculateAmountWithComission(decimal amount, decimal rate, decimal commission)
        {
            commission = 1 - commission;
            return amount * rate * commission;

        }
    }
}
