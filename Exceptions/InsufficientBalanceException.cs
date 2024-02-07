using System.Net;

namespace CryptoExchange.Exceptions
{
    public class InsufficientBalanceException : BaseException
    {
        public InsufficientBalanceException(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
