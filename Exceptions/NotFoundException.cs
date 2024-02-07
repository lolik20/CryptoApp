using System.Net;

namespace CryptoExchange.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}
