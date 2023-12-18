using System.Net;

namespace CryptoExchange.Exceptions
{
    public class CalculatingException : BaseException
    {
        public CalculatingException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
