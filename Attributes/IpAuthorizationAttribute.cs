using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoExchange.Attributes
{
    public class IpAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
     
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IMemoryCache _memoryCache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

            HttpContext httpContext = context.HttpContext;
            var clientIp = httpContext.Connection.RemoteIpAddress;
            if (clientIp == null)
            {
                throw new AccessForbiddenException("Ooops... Where is ur ip?");
            }
            var clientHost = httpContext.Request.Host.Host;
            if (clientHost == null)
            {
                throw new AccessForbiddenException("Please provide host url");

            }
            User user;

            var isHost = _memoryCache.TryGetValue(clientHost, out user);
            if (!isHost)
            {
                throw new AccessForbiddenException("Please register website url on gogo.cash");

            }
            
            if (clientHost != user.WebsiteUrl)
            {
                throw new AccessForbiddenException("Host not valid");
            }
           
        }
    }
}
