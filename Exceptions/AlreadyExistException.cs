using System.Net;

namespace CryptoExchange.Exceptions
{
    public class AlreadyExistException : BaseException
    {
        public AlreadyExistException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
