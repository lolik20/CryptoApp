using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class NetworkQuery : IRequestHandler<NetworkRequest, List<NetworkResponse>>
    {
        private readonly ApplicationContext _context;
        public NetworkQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<NetworkResponse>> Handle(NetworkRequest request, CancellationToken cancellationToken)
        {
            var networks = await _context.Networks.OrderBy(x => x.Id).Select(x => new NetworkResponse
            {
                ChainId = x.ChainId,
                ExplorerUrl = x.ExplorerUrl,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Symbol = x.Symbol,
                Url = x.Url,
            }).ToListAsync();
            return networks;
        }
    }
}
