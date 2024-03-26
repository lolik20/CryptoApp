using System.Net;

namespace CryptoExchange.Exceptions
{
    public class AccessForbiddenException : BaseException
    {
        public AccessForbiddenException(string message) : base(message, HttpStatusCode.Forbidden)
        {
        }
    }
}
