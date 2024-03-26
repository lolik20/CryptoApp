using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class CountryQuery : IRequestHandler<CountryRequest, List<CountryResponse>>
    {
        private readonly ApplicationContext _context;
        public CountryQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<CountryResponse>> Handle(CountryRequest request, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries.Select(x => new CountryResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            if (!countries.Any())
            {
                throw new NotFoundException("Countries not found");
            }
            return new List<CountryResponse>();
        }
    }
}
