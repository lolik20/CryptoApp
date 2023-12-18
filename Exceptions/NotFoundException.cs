using System.Net;

namespace CryptoExchange.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
