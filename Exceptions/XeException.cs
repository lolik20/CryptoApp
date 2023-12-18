using System.Net;

namespace CryptoExchange.Exceptions
{
    public class XeException : BaseException
    {
        public XeException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
