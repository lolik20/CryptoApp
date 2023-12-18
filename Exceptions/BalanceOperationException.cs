using System.Net;

namespace CryptoExchange.Exceptions
{
    public class BalanceOperationException : BaseException
    {
        public BalanceOperationException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
