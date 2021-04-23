using System;

namespace Shared.CustomException
{
    public class HttpException: Exception
    {
        public HttpException(string message, Exception ex):base(message,ex)
        {
            
        }

        public HttpException(string message):base(message)
        {
            
        }
    }
}