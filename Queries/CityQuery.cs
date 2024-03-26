using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class CityQuery : IRequestHandler<GetCitiesRequest, List<CityResponse>>
    {
        private readonly ApplicationContext _context;
        public CityQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<CityResponse>> Handle(GetCitiesRequest request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities.Select(x => new CityResponse
            {
                CountryId = x.CountryId,
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            if(cities.Any())
            {
                return cities;
            }

            throw new NotFoundException("Cities not found");
        }
    }
}
