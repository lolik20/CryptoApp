using CryptoExchange.Entities;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class CurrencyQuery : IRequestHandler<CurrencyRequest, List<CurrencyResponse>>
    {
        private readonly ApplicationContext _context;
        public CurrencyQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<CurrencyResponse>> Handle(CurrencyRequest request, CancellationToken cancellationToken)
        {
            
            var currencies = _context.Currencies.OrderBy(x => x.Id).Select(x => new CurrencyResponse
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Type = x.Type,
                Rate = 1.025m
            });

            if (request.currencyTypes != null && request.currencyTypes.Length > 0)
            {
                currencies = currencies.Where(x => request.currencyTypes.Contains(x.Type));
            }
            var result = await currencies.ToListAsync();
            return result;
        }
    }
}
