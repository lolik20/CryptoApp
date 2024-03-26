
using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoExchange.Workers
{
    public class UserCacheWorker : BackgroundService
    {
        private readonly ApplicationContext _context;
        private readonly IMemoryCache _memoryCache;
        public UserCacheWorker(ApplicationContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                _memoryCache.Set(user.WebsiteUrl, user);
            }
        }
    }
}
