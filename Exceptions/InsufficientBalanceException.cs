using System.Net;

namespace CryptoExchange.Exceptions
{
    public class InsufficientBalanceException : BaseException
    {
        public InsufficientBalanceException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
