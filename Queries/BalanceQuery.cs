using CryptoExchange.Entities;
using CryptoExchange.Models;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using CryptoExchange.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class BalanceQuery : IRequestHandler<BalanceRequest, List<BalanceResponse>>
    {
        private readonly ApplicationContext _context;
        public BalanceQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<BalanceResponse>> Handle(BalanceRequest request, CancellationToken cancellationToken)
        {
            var balances = await _context.Balances.AsNoTracking().Where(x => x.UserId == request.UserId).Include(x => x.Currency).Select(x => new BalanceResponse
            {
                Amount = x.Value,
                Currency = x.Currency.Name,
                CurrencyType = x.Currency.Type.ToString(),
                CurrencyId = x.CurrencyId

            }).ToListAsync();
            if (request.isZeroBalances)
            {
                var zeroBalances = await _context.Currencies.AsNoTracking().Select(x => new BalanceResponse
                {
                    Currency = x.Name,
                    CurrencyType = x.Type.ToString(),
                    Amount = 0,
                    CurrencyId = x.Id
                }).ToListAsync();
                balances = balances.Union(zeroBalances).DistinctBy(x => x.Currency).ToList();
            }

            return balances;
        }
    }
}
