using System.Net;

namespace CryptoExchange.Exceptions
{
    public class AlreadyExistException : BaseException
    {
        public AlreadyExistException(string message) : base(message, HttpStatusCode.Conflict)
        {
        }
    }
}
