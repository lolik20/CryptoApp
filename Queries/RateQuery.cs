using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class RateQuery : IRequestHandler<RateRequest, RateResponse>
    {
        private readonly ApplicationContext _context;
        public RateQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<RateResponse> Handle(RateRequest request, CancellationToken cancellationToken)
        {
            var fromCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == request.From.ToLower());
            if (fromCurrency == null)
            {
                throw new NotFoundException("From currency not found");
            }
            var toCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code == request.To.ToLower());
            if (toCurrency == null)
            {
                throw new NotFoundException("To currency not found");

            }

            throw new NotImplementedException();
        }
    }
}
