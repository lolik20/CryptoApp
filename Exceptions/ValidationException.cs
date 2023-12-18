using System.Net;

namespace CryptoExchange.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message, HttpStatusCode code) : base(message, code)
        {
        }

    }
}
