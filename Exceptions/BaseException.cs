using System.Net;

namespace CryptoExchange.Exceptions
{
    public class BaseException : Exception
    {
        private HttpStatusCode _code;
        public BaseException(string message,HttpStatusCode code) : base(message)
        {
            _code = code;
        }
        
        public int GetStatusCode ()
        {
            return (int)_code;
        }
    }
}
