using System.Net;

namespace CryptoExchange.Exceptions
{
    public class CalculatingException : BaseException
    {
        public CalculatingException(string message) : base(message, HttpStatusCode.InternalServerError)
        {
        }
    }
}
