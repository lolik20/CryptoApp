using System.Net;

namespace CryptoExchange.Exceptions
{
    public class ConvertException : BaseException
    {
        public ConvertException(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
