using System.Net;

namespace CryptoExchange.Exceptions
{
    public class ConvertException : BaseException
    {
        public ConvertException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
